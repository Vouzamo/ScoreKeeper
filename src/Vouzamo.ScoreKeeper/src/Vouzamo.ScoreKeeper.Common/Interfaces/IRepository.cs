using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Patterns.Specification.Interfaces;

namespace Vouzamo.ScoreKeeper.Common.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IObjectResponse<T> Get(Guid id);
        IObjectResponse<T> Post(T entity);
        IObjectResponse<T> Put(T entity, Guid id);
        IResponse Delete(Guid id);
        IObjectResponse<IPagedEnumerable<T>> List(int page, int itemsPerPage);
        IObjectResponse<IPagedEnumerable<T>> Query(ISpecification<T> specification, int page, int itemsPerPage);
    }

    public interface IAsyncRepository<T> where T : IEntity
    {
        Task<T> Get(Guid id);
        Task<T> Post(T entity);
        Task Put(T entity, Guid id);
        Task Delete(Guid id);
        Task<IPagedEnumerable<T>> List(int page, int itemsPerPage);
        Task<IPagedEnumerable<T>> Query(ISpecification<T> specification, int page, int itemsPerPage);

        IAggregateRepository<TAggregate> Repository<TAggregate>(Expression<Func<TAggregate, Guid>> propertyExpression, Guid id) where TAggregate : class, IAggregate;
    }

    public interface IAggregateRepository<T> : IAsyncRepository<T> where T : IAggregate
    {

    }

    public interface IAggregateRootRepository<T> : IAsyncRepository<T> where T : IAggregateRoot
    {

    }
}
