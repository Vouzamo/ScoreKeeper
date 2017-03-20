using System;
using Vouzamo.ScoreKeeper.Common.Interfaces.Services;

namespace Vouzamo.ScoreKeeper.Core.Services
{
    public class GenerateFixtureSettings : IGenerateFixtureSettings
    {
        public FixtureType FixtureType { get; set; }
        public int NumberOfEncounters { get; set; }
        public bool IncludeReverseFixtures { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan TimeBetweenFixtures { get; set; }

        public GenerateFixtureSettings()
        {
            FixtureType = FixtureType.RoundRobin;
            NumberOfEncounters = 1;
            IncludeReverseFixtures = true;
            Start = DateTime.Now;
            TimeBetweenFixtures = TimeSpan.FromDays(7);
        }
    }
}
