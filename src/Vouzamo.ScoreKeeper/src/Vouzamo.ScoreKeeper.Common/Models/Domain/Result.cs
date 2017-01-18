using System;
using System.Collections.ObjectModel;
using Vouzamo.ScoreKeeper.Common.Models.Infrastructure;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Result : Entity<Guid>
    {
        public Fixture Fixture { get; set; }

        public Collection<Game> Games { get; set; }

        public Result()
        {
            Games = new Collection<Game>();
        }
    }
}
