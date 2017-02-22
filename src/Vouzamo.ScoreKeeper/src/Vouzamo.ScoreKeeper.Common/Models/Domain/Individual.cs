namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class Individual : AggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

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
