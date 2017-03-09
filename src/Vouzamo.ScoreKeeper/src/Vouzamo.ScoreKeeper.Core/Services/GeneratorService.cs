using System.Collections.Generic;
using System.Linq;
using Vouzamo.ScoreKeeper.Common.Interfaces.Services;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Core.Services
{
    public class GeneratorService : IGeneratorService
    {
        public IEnumerable<Fixture> GenerateFixtures(Season season, IList<Team> teams, IGenerateFixtureSettings settings)
        {
            var fixtures = new List<Fixture>();

            var otherTeams = new Queue<Team>(teams.Skip(1));

            if (otherTeams.Count % 2 == 0)
            {
                otherTeams.Enqueue(Team.Bye);
            }

            var fixtureDate = settings.Start;
            var totalEncounters = settings.IncludeReverseFixtures ? settings.NumberOfEncounters * 2 : settings.NumberOfEncounters;

            for (var encounter = 0; encounter < totalEncounters; encounter++)
            {
                var reverseFixtures = settings.IncludeReverseFixtures && encounter % 2 == 1;

                for (var fixtureNumber = 0; fixtureNumber < otherTeams.Count; fixtureNumber++)
                {
                    // determine fixtures
                    var tempFixtures = new List<Fixture>
                    {
                        new Fixture(fixtureDate, teams.First(), otherTeams.First(), season.Id)
                    };

                    for (var team = 1; team < otherTeams.Count - 1; team += 2)
                    {
                        var home = otherTeams.ElementAt(team);
                        var away = otherTeams.ElementAt(team + 1);

                        tempFixtures.Add(new Fixture(fixtureDate, home, away, season.Id));
                    }

                    if (reverseFixtures)
                    {
                        foreach (var fixture in tempFixtures)
                        {
                            var tempId = fixture.HomeTeamId;
                            fixture.HomeTeamId = fixture.AwayTeamId;
                            fixture.AwayTeamId = tempId;
                        }
                    }

                    fixtures.AddRange(tempFixtures);
                    
                    // rotate teams
                    otherTeams.Enqueue(otherTeams.Dequeue());

                    // increase date
                    fixtureDate = fixtureDate.Add(settings.TimeBetweenFixtures);
                }
            }

            return fixtures;
        }
    }
}
