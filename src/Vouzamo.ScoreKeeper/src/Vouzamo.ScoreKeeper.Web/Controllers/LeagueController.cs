using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces.Services;
using Vouzamo.ScoreKeeper.Common.Models.Domain;
using Vouzamo.ScoreKeeper.Common.Models.View;
using Vouzamo.ScoreKeeper.Core.Infrastructure;
using Vouzamo.ScoreKeeper.Core.Services;
using Vouzamo.ScoreKeeper.Core.Specifications;
using Vouzamo.ScoreKeeper.Web.Extensions;

namespace Vouzamo.ScoreKeeper.Web.Controllers
{
    [Route("api/leagues")]
    public class LeagueController : ApiController<League>
    {
        private IGeneratorService GeneratorService { get; set; }

        private ApiController<Season> LeagueSeasons(Guid leagueId) => Aggregate(new AggregateParentSpecification<Season>(x => x.LeagueId, leagueId));
        private ApiController<Team> LeagueTeams(Guid leagueId) => Aggregate(new AggregateParentSpecification<Team>(x => x.LeagueId, leagueId));
        private ApiController<Fixture> SeasonFixtures(Guid leagueId, Guid seasonId) => LeagueSeasons(leagueId).Aggregate(new AggregateParentSpecification<Fixture>(x => x.SeasonId, seasonId));
        private ApiController<TeamMember> TeamMembers(Guid leagueId, Guid teamId) => LeagueTeams(leagueId).Aggregate(new AggregateParentSpecification<TeamMember>(x => x.TeamId, teamId));
        private ApiController<Game> FixtureGames(Guid leagueId, Guid seasonId, Guid fixtureId) => SeasonFixtures(leagueId, seasonId).Aggregate(new AggregateParentSpecification<Game>(x => x.FixtureId, fixtureId));

        public LeagueController(IUnitOfWorkContext context, IGeneratorService generatorService) : base(context)
        {
            GeneratorService = generatorService;
        }

        public async Task<IActionResult> Test1(IUnitOfWorkContext context, League league)
        {
            return await context.Run(new CreateTransaction<League>(league)).Handle<IActionResult, League>(s => new CreatedResult(s.Id.ToString(), s), e => new BadRequestObjectResult(e));
        }

        public async Task<IActionResult> Test2(IUnitOfWorkContext context, League league)
        {
            // Transaction Scope
            return await context.Run(async transaction =>
            {
                // Command Scope
                return await transaction.Run(async command =>
                {
                    // Atomic Commands
                    return await command.Post(league);
                });

            // Convert to an IActionResult (handle Exceptions)
            }).Handle<IActionResult, League>(s => new CreatedResult(s.Id.ToString(), s), e => new BadRequestObjectResult(e));
        }

        #region Seasons
        [HttpGet("{leagueId}/seasons")]
        public async Task<IActionResult> GetSeasons(Guid leagueId, int page = 1)
        {
            return await LeagueSeasons(leagueId).List(page);
        }

        [HttpGet("{leagueId}/seasons/{id}")]
        public async Task<IActionResult> GetSeason(Guid leagueId, Guid id)
        {
            return await LeagueSeasons(leagueId).Get(id);
        }

        [HttpPost("{leagueId}/seasons")]
        public async Task<IActionResult> PostSeason(Guid leagueId, [FromBody] Season season)
        {
            return await LeagueSeasons(leagueId).Post(season);
        }

        [HttpPut("{leagueId}/seasons/{id}")]
        public async Task<IActionResult> PutSeason(Guid leagueId, [FromBody] Season season, Guid id)
        {
            return await LeagueSeasons(leagueId).Put(season, id);
        }

        [HttpDelete("{leagueId}/seasons/{id}")]
        public async Task<IActionResult> DeleteSeason(Guid leagueId, Guid id)
        {
            return await LeagueSeasons(leagueId).Delete(id);
        }

        [HttpPost("{leagueId}/seasons/{id}/generate-fixtures")]
        public async Task<IActionResult> GenerateFixtures(Guid leagueId, Guid id, [FromBody] GenerateFixtureSettings settings)
        {
            var unitOfWork = new GenerateFixturesUnitOfWork(GeneratorService, settings, leagueId, id);

            var task = UnitOfWorkContext.Run(unitOfWork);

            return await task.Handle<IActionResult, ServiceModel<IGenerateFixtureSettings, IEnumerable<Fixture>>>(s => new ObjectResult(s), e => new BadRequestObjectResult(e)); ;
        }
        #endregion

        #region Teams
        [HttpGet("{leagueId}/teams")]
        public async Task<IActionResult> GetTeams(Guid leagueId, int page = 1)
        {
            return await LeagueTeams(leagueId).List(page);
        }

        [HttpGet("{leagueId}/teams/{id}")]
        public async Task<IActionResult> GetTeam(Guid leagueId, Guid id)
        {
            return await LeagueTeams(leagueId).Get(id);
        }

        [HttpPost("{leagueId}/teams")]
        public async Task<IActionResult> PostTeam(Guid leagueId, [FromBody] Team team)
        {
            return await LeagueTeams(leagueId).Post(team);
        }
        #endregion

        #region TeamMembers
        [HttpGet("{leagueId}/teams/{teamId}/members")]
        public async Task<IActionResult> GetTeamMembers(Guid leagueId, Guid teamId, int page = 1)
        {
            return await TeamMembers(leagueId, teamId).List(page);
        }

        [HttpGet("{leagueId}/teams/{teamId}/members/{id}")]
        public async Task<IActionResult> GetTeam(Guid leagueId, Guid teamId, Guid id)
        {
            return await TeamMembers(leagueId, teamId).Get(id);
        }
        #endregion

        #region Fixtures
        [HttpGet("{leagueId}/seasons/{seasonId}/fixtures")]
        public async Task<IActionResult> GetFixtures(Guid leagueId, Guid seasonId, int page = 1)
        {
            return await SeasonFixtures(leagueId, seasonId).List(page);
        }

        [HttpGet("{leagueId}/seasons/{seasonId}/fixtures/{id}")]
        public async Task<IActionResult> GetSeason(Guid leagueId, Guid seasonId, Guid id)
        {
            return await SeasonFixtures(leagueId, seasonId).Get(id);
        }
        #endregion

        #region Games
        [HttpGet("{leagueId}/seasons/{seasonId}/fixtures/{fixtureId}/games")]
        public async Task<IActionResult> GetGames(Guid leagueId, Guid seasonId, Guid fixtureId, int page = 1)
        {
            return await FixtureGames(leagueId, seasonId, fixtureId).List(page);
        }

        [HttpGet("{leagueId}/seasons/{seasonId}/fixtures/{fixtureId}/games/{id}")]
        public async Task<IActionResult> GetGames(Guid leagueId, Guid seasonId, Guid fixtureId, Guid id)
        {
            return await FixtureGames(leagueId, seasonId, fixtureId).Get(id);
        }
        #endregion
    }
}
