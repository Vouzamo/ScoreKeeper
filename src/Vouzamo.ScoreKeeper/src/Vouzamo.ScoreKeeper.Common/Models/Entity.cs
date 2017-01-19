using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Common.Models
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
