using System;
using System.Collections.Generic;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Common.Interfaces.Services
{
    public interface IGeneratorService
    {
        IEnumerable<Fixture> GenerateFixtures(Season season, IList<Team> teams, IGenerateFixtureSettings settings);
    }

    public interface IGenerateFixtureSettings
    {
        FixtureType FixtureType { get; }
        int NumberOfEncounters { get; }
        bool IncludeReverseFixtures { get; }
        DateTime Start { get; }
        TimeSpan TimeBetweenFixtures { get; }
    }

    public enum FixtureType
    {
        RoundRobin
    }
}
