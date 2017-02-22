using System;
using Microsoft.AspNetCore.Mvc;

namespace Vouzamo.ScoreKeeper.Web.Attributes
{
    public class AggregateApiRouteAttribute : AggregateRootApiRouteAttribute
    {
        public AggregateApiRouteAttribute(string template, Type aggregate, Type parent) : base(template.Replace("[parent]", parent.Name), aggregate)
        {
            
        }
    }

    public class AggregateRootApiRouteAttribute : RouteAttribute
    {
        public AggregateRootApiRouteAttribute(string template, Type aggregate) : base(template.Replace("[aggregate]", aggregate.Name))
        {

        }
    }
}
