using System;
using System.Collections.ObjectModel;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Team : Entity<Guid>
    {
        public string Name { get; set; }
        public Collection<Individual> Members { get; set; }

        public Team()
        {
            Members = new Collection<Individual>();
        }
    }
}
