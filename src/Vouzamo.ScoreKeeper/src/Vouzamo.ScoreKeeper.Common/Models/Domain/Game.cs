using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Game : Entity<Guid>
    {
        public Guid HomeTeamMemberId { get; set; }
        public Guid AwayTeamMemberId { get; set; }

        public Guid FixtureId { get; set; }

        protected Game()
        {
            
        }

        public Game(TeamMember home, TeamMember away, Fixture fixture) : this()
        {
            HomeTeamMemberId = home.Id;
            AwayTeamMemberId = away.Id;
            FixtureId = fixture.Id;
        }
    }
}
