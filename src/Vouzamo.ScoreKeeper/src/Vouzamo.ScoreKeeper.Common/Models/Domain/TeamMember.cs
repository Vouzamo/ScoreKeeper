using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class TeamMember : Entity<Guid>
    {
        public Guid TeamId { get; set; }
        public Guid IndividualId { get; set; }
        public int Handicap { get; set; }

        protected TeamMember()
        {
            
        }

        public TeamMember(Team team, Individual individual, int handicap = 0) : this()
        {
            TeamId = team.Id;
            IndividualId = individual.Id;
            Handicap = handicap;
        }
    }
}
