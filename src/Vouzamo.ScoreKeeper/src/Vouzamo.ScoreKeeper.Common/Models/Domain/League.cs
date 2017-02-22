namespace Vouzamo.ScoreKeeper.Common.Models.Domain
{
    public class League : AggregateRoot
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
