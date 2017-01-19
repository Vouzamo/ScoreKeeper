using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class League : Entity<Guid>
    {
        public string Name { get; set; }

        protected League()
        {
            
        }

        public League(string name) : this()
        {
            Name = name;
        }
    }
}
