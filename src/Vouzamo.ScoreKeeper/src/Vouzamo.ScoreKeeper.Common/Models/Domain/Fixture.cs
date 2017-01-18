using System;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Fixture : Entity<Guid>
    {
        public DateTime Date { get; set; }
        public Team Home { get; set; }
        public Team Away { get; set; }
    }
}
