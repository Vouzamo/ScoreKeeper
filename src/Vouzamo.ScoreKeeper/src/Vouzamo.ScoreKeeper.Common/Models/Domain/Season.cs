using System;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Season : Entity<Guid>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
