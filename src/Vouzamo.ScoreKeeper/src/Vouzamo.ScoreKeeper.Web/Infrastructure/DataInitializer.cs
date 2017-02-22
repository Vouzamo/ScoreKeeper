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

            if (!context.Set<Individual>().Any())
            {
                context.Set<Individual>().Add(individual);
                context.SaveChanges();
            }

            // League
            var league = new League("Workmen's Hall of Norwood");

            if (!context.Set<League>().Any())
            {
                context.Set<League>().Add(league);
                context.SaveChanges();

                //Team
                var team = new Team("My Team", league.Id);

                if (!context.Set<Team>().Any())
                {
                    context.Set<Team>().Add(team);
                    context.SaveChanges();

                    // Team Member
                    var teamMember = new TeamMember(team, individual, 96);

                    if (!context.Set<TeamMember>().Any())
                    {
                        context.Set<TeamMember>().Add(teamMember);
                        context.SaveChanges();

                        // Season
                        var season = new Season(DateTime.Today.Subtract(TimeSpan.FromDays(20)), DateTime.Today.Add(TimeSpan.FromDays(20)), league.Id);

                        if (!context.Set<Season>().Any())
                        {
                            context.Set<Season>().Add(season);
                            context.SaveChanges();

                            // Fixture
                            var fixture = new Fixture(DateTime.Today, team, team, season.Id);

                            if (!context.Set<Fixture>().Any())
                            {
                                context.Set<Fixture>().Add(fixture);
                                context.SaveChanges();

                                // Game
                                var game = new Game(teamMember, teamMember, fixture.Id);

                                if (!context.Set<Game>().Any())
                                {
                                    context.Set<Game>().Add(game);
                                    context.SaveChanges();

                                    // Rack
                                    var rack = new Rack(14, 6, game.Id);

                                    if (!context.Set<Rack>().Any())
                                    {
                                        context.Set<Rack>().Add(rack);
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
