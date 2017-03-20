using System;
using System.Threading.Tasks;
using Patterns.Specification.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Common.Interfaces
{
    public interface IContext
    {
        void Add<T>(T entity) where T : class, IEntity;
        void SaveChanges();
    }

    public interface IScoped<in TContext>
    {
        Task Run(TContext context);
    }

    public interface IScoped<T, in TContext>
    {
        Task<T> Run(TContext context);
    }

    public interface IScopedRunner<out TScopeContext>
    {
        Task Run(IScoped<TScopeContext> scoped);
        Task Run(Func<TScopeContext, Task> scoped);

        Task<T> Run<T>(IScoped<T, TScopeContext> scoped);
        Task<T> Run<T>(Func<TScopeContext, Task<T>> scoped);
    }

    public interface IUnitOfWorkContext : IScopedRunner<ITransactionContext>
    {

    }

    public interface IUnitOfWork<T> : IScoped<T, ITransactionContext>
    {
        
    }

    public interface IAtomicContext
    {
        Task<T> Post<T>(T entity) where T : class, IEntity;
        Task<T> Put<T>(Guid id, T entity) where T : class, IEntity;
        Task Delete<T>(T entity) where T : class, IEntity;
        Task Delete<T>(Guid id) where T : class, IEntity;
    }

    public interface IAtomicCommand<T> : IScoped<T, IAtomicContext>
    {
        
    }

    public interface ITransactionContext : IScopedRunner<IAtomicContext>
    {
        Task<T> Get<T>(Guid id) where T : class, IEntity;
        Task<IPagedEnumerable<T>> List<T>(int page, int itemsPerPage) where T : class, IEntity;
        Task<IPagedEnumerable<T>> Query<T>(ISpecification<T> specification, int page, int itemsPerPage) where T : class, IEntity;
    }

    public interface IUnitOfWork : IScoped<ITransactionContext>
    {

    }

    public class CreateCommand<T> : IAtomicCommand<T> where T : class, IEntity
    {
        public T Entity { get; set; }

        public CreateCommand(T entity)
        {
            Entity = entity;
        }

        public async Task<T> Run(IAtomicContext context)
        {
            return await context.Post(Entity);
        }
    }

    public class CreateTransaction<T> : IUnitOfWork<T> where T : class, IEntity
    {
        public T Entity { get; set; }

        public CreateTransaction(T entity)
        {
            Entity = entity;
        }

        public async Task<T> Run(ITransactionContext context)
        {
            var deleteMethod1 = await context.Run(x => x.Post(Entity));
            //var deleteMethod2 = await context.Run(new CreateCommand<T>(Entity));

            return deleteMethod1;
        }
    }


    public class RemoveIndividual : IUnitOfWork
    {
        public Individual Individual { get; set; }

        public RemoveIndividual(Individual individual)
        {
            Individual = individual;
        }

        public async Task Run(ITransactionContext context)
        {
            await context.Run(async x => await x.Delete(Individual));
        }
    }
}
