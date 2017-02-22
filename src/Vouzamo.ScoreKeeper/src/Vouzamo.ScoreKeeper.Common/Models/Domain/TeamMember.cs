using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class TeamMember : Aggregate
    {
        public Guid IndividualId { get; set; }
        public int Handicap { get; set; }
        public Guid TeamId { get; set; }

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
