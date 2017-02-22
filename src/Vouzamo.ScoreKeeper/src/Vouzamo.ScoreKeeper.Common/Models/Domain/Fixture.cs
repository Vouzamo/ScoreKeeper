using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Fixture : Aggregate
    {
        public DateTime Date { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid SeasonId { get; set; }

        protected Fixture()
        {
            
        }

        public Fixture(DateTime date, Team home, Team away, Guid season) : this()
        {
            Date = date;
            HomeTeamId = home.Id;
            AwayTeamId = away.Id;
            SeasonId = season;
        }
    }
}
