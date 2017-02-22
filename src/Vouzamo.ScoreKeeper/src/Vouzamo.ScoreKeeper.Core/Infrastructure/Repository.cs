using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patterns.Specification.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Core.Specifications;

namespace Vouzamo.ScoreKeeper.Core.Infrastructure
{
    public abstract class AsyncRepository<T> : IAsyncRepository<T> where T : class, IEntity
    {
        protected DbContext Context { get; set; }
        protected abstract IQueryable<T> Queryable { get; }

        protected AsyncRepository(DbContext context)
        {
            Context = context;
        }

        public async Task<T> Get(Guid id)
        {
            return await Queryable.SingleAsync(x => x.Id == id);
        }

        public async Task<T> Post(T entity)
        {
            entity.Id = Guid.NewGuid();

            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task Put(T entity, Guid id)
        {
            entity.Id = id;

            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = Queryable.Single(x => x.Id == id);

            Context.Entry(entity).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public Task<IPagedEnumerable<T>> List(int page, int itemsPerPage)
        {
            return Queryable.ToPagedEnumerableAsync(page, itemsPerPage);
        }

        public Task<IPagedEnumerable<T>> Query(ISpecification<T> specification, int page, int itemsPerPage)
        {
            return Queryable.Where(x => specification.IsSatisfiedBy(x)).ToPagedEnumerableAsync(page, itemsPerPage);
        }

        public abstract IAggregateRepository<TAggregate> Repository<TAggregate>(Expression<Func<TAggregate, Guid>> propertyExpression, Guid id) where TAggregate : class, IAggregate;
    }

    public class AggregateRootRepository<T> : AsyncRepository<T>, IAggregateRootRepository<T> where T : class, IAggregateRoot
    {
        protected override IQueryable<T> Queryable => Context.Set<T>();

        public AggregateRootRepository(DbContext context) : base(context)
        {
            
        }

        public override IAggregateRepository<TAggregate> Repository<TAggregate>(Expression<Func<TAggregate, Guid>> propertyExpression, Guid id)
        {
            return new AggregateRepository<TAggregate>(Context, propertyExpression, id);
        }
    }

    public class AggregateRepository<T> : AsyncRepository<T>, IAggregateRepository<T> where T : class, IAggregate
    {
        protected override IQueryable<T> Queryable => Context.Set<T>().Where(x => new AggregateParentSpecification<T>(ParentSelector, ParentId).IsSatisfiedBy(x));
        protected Expression<Func<T, Guid>> ParentSelector { get; }
        protected Guid ParentId { get; }

        public AggregateRepository(DbContext context, Expression<Func<T, Guid>> propertySelector, Guid parentId) : base(context)
        {
            ParentSelector = propertySelector;
            ParentId = parentId;
        }

        public override IAggregateRepository<TAggregate> Repository<TAggregate>(Expression<Func<TAggregate, Guid>> propertyExpression, Guid id)
        {
            return new AggregateRepository<TAggregate>(Context, propertyExpression, id);
        }
    }
}
