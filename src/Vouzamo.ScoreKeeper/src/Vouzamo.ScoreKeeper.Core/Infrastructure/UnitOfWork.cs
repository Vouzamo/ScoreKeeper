﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patterns.Specification.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces.Services;
using Vouzamo.ScoreKeeper.Common.Models.Domain;
using Vouzamo.ScoreKeeper.Common.Models.View;
using Vouzamo.ScoreKeeper.Core.Services;
using Vouzamo.ScoreKeeper.Core.Specifications;

namespace Vouzamo.ScoreKeeper.Core.Infrastructure
{
    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        protected DbContext Context { get; set; }

        protected ITransactionContext TransactionContext => new TransactionContext(Context);

        public UnitOfWorkContext(DbContext context)
        {
            Context = context;
        }

        public async Task Run(IScoped<ITransactionContext> scoped)
        {
            await Run(scoped.Run);
        }

        public async Task Run(Func<ITransactionContext, Task> scoped)
        {
            using (var transactionScope = await Context.Database.BeginTransactionAsync())
            {
                await scoped.Invoke(TransactionContext);

                transactionScope.Commit();
            }
        }

        public async Task<T> Run<T>(IScoped<T, ITransactionContext> transaction)
        {
            return await Run(transaction.Run);
        }

        public async Task<T> Run<T>(Func<ITransactionContext, Task<T>> transaction)
        {
            using (var transactionScope = await Context.Database.BeginTransactionAsync())
            {
                var result = await transaction.Invoke(TransactionContext);

                transactionScope.Commit();

                return result;
            }
        }
    }

    public class TransactionContext : ITransactionContext
    {
        protected DbContext Context { get; set; }

        protected IAtomicContext AtomicContext => new AtomicContext(Context);

        public TransactionContext(DbContext context)
        {
            Context = context;
        }

        public async Task<T> Get<T>(Guid id) where T : class, IEntity
        {
            return await Task.Run(() =>
            {
                return Context.Set<T>().Single(x => x.Id == id);
            });
        }

        public async Task<IPagedEnumerable<T>> List<T>(int page, int itemsPerPage) where T : class, IEntity
        {
            return await Context.Set<T>().ToPagedEnumerableAsync(page, itemsPerPage);
        }

        public async Task<IPagedEnumerable<T>> Query<T>(ISpecification<T> specification, int page, int itemsPerPage) where T : class, IEntity
        {
            return await Context.Set<T>().Where(x => specification.IsSatisfiedBy(x)).ToPagedEnumerableAsync(page, itemsPerPage);
        }

        public async Task Run(IScoped<IAtomicContext> scoped)
        {
            await Run(scoped.Run);
        }

        public async Task Run(Func<IAtomicContext, Task> scoped)
        {
            await scoped.Invoke(AtomicContext);

            await Context.SaveChangesAsync();
        }

        public async Task<T> Run<T>(IScoped<T, IAtomicContext> command)
        {
            return await Run(command.Run);
        }

        public async Task<T> Run<T>(Func<IAtomicContext, Task<T>> command)
        {
            var result = await command.Invoke(AtomicContext);

            await Context.SaveChangesAsync();

            return result;
        }
    }

    public class AtomicContext : IAtomicContext
    {
        protected DbContext Context { get; set; }

        public AtomicContext(DbContext context)
        {
            Context = context;
        }

        public async Task Delete<T>(T entity) where T : class, IEntity
        {
            await Task.Run(() =>
            {
                Context.Entry(entity).State = EntityState.Deleted;
            });
        }

        public async Task Delete<T>(Guid id) where T : class, IEntity
        {
            var entity = Context.Set<T>().Single(x => x.Id == id);

            await Delete(entity);
        }

        public async Task<T> Post<T>(T entity) where T : class, IEntity
        {
            return await Task.Run(() =>
            {
                entity.Id = Guid.NewGuid();

                Context.Entry(entity).State = EntityState.Added;

                return entity;
            });
        }

        public async Task<T> Put<T>(Guid id, T entity) where T : class, IEntity
        {
            return await Task.Run(() =>
            {
                entity.Id = id;

                Context.Entry(entity).State = EntityState.Modified;

                return entity;
            });
        }
    }

    public class GenerateFixturesUnitOfWork : IUnitOfWork<ServiceModel<IGenerateFixtureSettings, IEnumerable<Fixture>>>
    {
        private IGeneratorService Service { get; set; }
        private IGenerateFixtureSettings Settings { get; set; }
        private Guid LeagueId { get; set; }
        private Guid SeasonId { get; set; }

        public GenerateFixturesUnitOfWork(IGeneratorService service, IGenerateFixtureSettings settings, Guid leagueId, Guid seasonId)
        {
            Service = service;
            Settings = settings;
            LeagueId = leagueId;
            SeasonId = seasonId;
        }

        public async Task<ServiceModel<IGenerateFixtureSettings, IEnumerable<Fixture>>> Run(ITransactionContext context)
        {
            var teamsSpecification = new AggregateParentSpecification<Team>(x => x.LeagueId, LeagueId);

            // Atomic Commands
            var season = await context.Get<Season>(SeasonId);
            var teams = await context.Query(teamsSpecification, 1, int.MaxValue);

            var result = Service.GenerateFixtures(season, teams.Enumerable.ToList(), Settings);

            return new ServiceModel<IGenerateFixtureSettings, IEnumerable<Fixture>>(Settings, result);
        }
    }
}
