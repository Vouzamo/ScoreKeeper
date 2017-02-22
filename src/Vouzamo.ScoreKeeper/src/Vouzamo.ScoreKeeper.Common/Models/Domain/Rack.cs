using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Rack : Aggregate
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public Guid GameId { get; set; }

        protected Rack()
        {
            
        }

        public Rack(int homeScore, int awayScore, Guid game) : this()
        {
            HomeScore = homeScore;
            AwayScore = awayScore;
            GameId = game;
        }
    }
}
