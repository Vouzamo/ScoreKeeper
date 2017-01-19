using System;
using System.Linq;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Web.Infrastructure
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Individual
            var individual = new Individual("John", "Askew");

            if (!context.List<Individual>().Any())
            {
                context.Post(individual);
                context.SaveChanges();
            }

            // League
            var league = new League("Workmen's Hall of Norwood");

            if (!context.List<League>().Any())
            {
                context.Post(league);
                context.SaveChanges();

                //Team
                var team = new Team("My Team", league);

                if (!context.List<Team>().Any())
                {
                    context.Post(team);
                    context.SaveChanges();

                    // Team Member
                    var teamMember = new TeamMember(team, individual, 96);

                    if (!context.List<TeamMember>().Any())
                    {
                        context.Post(teamMember);
                        context.SaveChanges();

                        // Season
                        var season = new Season(DateTime.Today.Subtract(TimeSpan.FromDays(20)), DateTime.Today.Add(TimeSpan.FromDays(20)), league);

                        if (!context.List<Season>().Any())
                        {
                            context.Post(season);
                            context.SaveChanges();

                            // Fixture
                            var fixture = new Fixture(DateTime.Today, team, team, season);

                            if (!context.List<Fixture>().Any())
                            {
                                context.Post(fixture);
                                context.SaveChanges();

                                // Game
                                var game = new Game(teamMember, teamMember, fixture);

                                if (!context.List<Game>().Any())
                                {
                                    context.Post(game);
                                    context.SaveChanges();

                                    // Rack
                                    var rack = new Rack(14, 6, game);

                                    if (!context.List<Rack>().Any())
                                    {
                                        context.Post(rack);
                                        context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
