using System.Linq;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Web.Infrastructure
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Leagues.Any())
            {
                var league = new League
                {
                    Name = "Test League"
                };

                context.Post(league);

                context.SaveChanges();
            }
        }
    }
}
