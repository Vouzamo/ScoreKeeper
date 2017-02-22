using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Vouzamo.ScoreKeeper.Common.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public interface IAggregateRoot : IEntity
    {

    }

    public interface IAggregate : IEntity
    {
        
    }
}
