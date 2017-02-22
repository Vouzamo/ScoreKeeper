using System;
using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Game : Aggregate
    {
        public Guid HomeTeamMemberId { get; set; }
        public Guid AwayTeamMemberId { get; set; }
        public Guid FixtureId { get; set; }

        protected Game()
        {
            
        }

        public Game(TeamMember home, TeamMember away, Guid fixture) : this()
        {
            HomeTeamMemberId = home.Id;
            AwayTeamMemberId = away.Id;
            FixtureId = fixture;
        }
    }
}
