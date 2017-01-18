using System.Linq;

namespace Vouzamo.ScoreKeeper.Common.Interfaces
{
    public interface IRepository<TId>
    {
        IQueryable<T> List<T>() where T : class, IEntity<TId>;
        T Get<T>(TId id) where T : class, IEntity<TId>;
        T Post<T>(T entity) where T : class, IEntity<TId>;
        T Put<T>(T entity, TId id) where T : class, IEntity<TId>;
        void Delete<T>(T entity) where T : class, IEntity<TId>;
    }
}
