using System;
using System.Collections.ObjectModel;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class League : Entity<Guid>
    {
        public string Name { get; set; }

        public Collection<Season> Seasons { get; set; }

        public League()
        {
            Seasons = new Collection<Season>();
        }
    }
}
