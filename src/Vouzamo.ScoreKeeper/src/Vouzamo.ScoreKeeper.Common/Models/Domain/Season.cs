using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Season : Aggregate
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid LeagueId { get; set; }

        public string Name => $"{Start.Year} / {End.Year}";

        protected Season()
        {
            
        }

        public Season(DateTime start, DateTime end, Guid league) : this()
        {
            Start = start;
            End = end;
            LeagueId = league;
        }
    }
}
