using System;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Handicap : Entity<Guid>
    {
        public Individual Player { get; set; }
        public int Value { get; set; }
    }
}
