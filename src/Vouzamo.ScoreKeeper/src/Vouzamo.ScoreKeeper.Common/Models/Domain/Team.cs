using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Team : Entity<Guid>
    {
        public string Name { get; set; }

        public Guid LeagueId { get; set; }

        protected Team()
        {
            
        }

        public Team(string name, League league) : this()
        {
            Name = name;
            LeagueId = league.Id;
        }
    }
}
