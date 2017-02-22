using System;
using System.Linq.Expressions;
using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Common.Models
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        
    }

    public abstract class Aggregate : Entity, IAggregate
    {
        
    }
}
