using System;
using System.Linq.Expressions;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Team : Aggregate
    {
        public string Name { get; set; }
        public Guid LeagueId { get; set; }

        protected Team()
        {
            
        }

        public Team(string name, Guid league) : this()
        {
            Name = name;
            LeagueId = league;
        }

        public Expression<Func<Team, Guid>> AggregateParentId(League parent)
        {
            return x => x.LeagueId;
        }
    }
}
