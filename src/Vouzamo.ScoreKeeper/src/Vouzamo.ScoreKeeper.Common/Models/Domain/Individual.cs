using System;

namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Individual : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        protected Individual()
        {
            
        }

        public Individual(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
