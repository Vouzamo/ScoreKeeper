using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Common.Models.Infrastructure
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }

        protected Entity()
        {
            Id = default(T);
        }
    }
}
