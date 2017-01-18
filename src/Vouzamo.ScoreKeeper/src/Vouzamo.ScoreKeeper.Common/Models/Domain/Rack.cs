using System;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Rack : Entity<Guid>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
