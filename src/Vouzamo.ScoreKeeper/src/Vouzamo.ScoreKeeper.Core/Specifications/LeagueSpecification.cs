using System;
using System.Linq.Expressions;
using Patterns.Specification.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Core.Specifications
{
    public class EmptySpecification<T> : ISpecification<T>
    {
        public bool IsSatisfiedBy(T subject)
        {
            return true;
        }
    }

    public class AggregateParentSpecification<T> : ISpecification<T> where T : IAggregate
    {
        protected Guid ParentId { get; set; }
        protected Func<T, Guid> PropertyExpression { get; }

        public AggregateParentSpecification(Expression<Func<T, Guid>> propertyExpression, Guid parentId)
        {
            ParentId = parentId;
            PropertyExpression = propertyExpression.Compile();
        }

        public bool IsSatisfiedBy(T subject)
        {
            return ParentId == PropertyExpression.Invoke(subject);
        }
    }
}
