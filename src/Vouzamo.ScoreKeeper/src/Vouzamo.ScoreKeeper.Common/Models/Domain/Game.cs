using System;
using System.Collections.ObjectModel;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Game : Entity<Guid>
    {
        public Individual Home { get; set; }
        public Individual Away { get; set; }

        public Collection<Rack> Racks { get; set; }

        public Game()
        {
            Racks = new Collection<Rack>();
        }
    }
}
