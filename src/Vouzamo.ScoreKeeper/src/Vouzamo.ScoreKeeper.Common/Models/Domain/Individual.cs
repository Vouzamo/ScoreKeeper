using System;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Individual : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
