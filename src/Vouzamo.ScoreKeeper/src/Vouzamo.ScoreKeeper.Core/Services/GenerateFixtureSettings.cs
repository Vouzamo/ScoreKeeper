using System;
using Vouzamo.ScoreKeeper.Common.Interfaces.Services;

namespace Vouzamo.ScoreKeeper.Core.Services
{
    public class GenerateFixtureSettings : IGenerateFixtureSettings
    {
        public FixtureType FixtureType { get; protected set; }
        public int NumberOfEncounters { get; protected set; }
        public bool IncludeReverseFixtures { get; protected set; }
        public DateTime Start { get; protected set; }
        public TimeSpan TimeBetweenFixtures { get; protected set; }

        public GenerateFixtureSettings(FixtureType fixtureType, int numberOfEncounters, bool includeReverseFixtures, DateTime start, TimeSpan timeBetweenFixtures)
        {
            FixtureType = fixtureType;
            NumberOfEncounters = numberOfEncounters;
            IncludeReverseFixtures = includeReverseFixtures;
            Start = start != default(DateTime) ? start : DateTime.Now;
            TimeBetweenFixtures = timeBetweenFixtures != default(TimeSpan) ? timeBetweenFixtures : TimeSpan.FromDays(7);
        }
    }
}
