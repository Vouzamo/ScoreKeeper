using Vouzamo.ScoreKeeper.Common.Models.Domain;
using Vouzamo.ScoreKeeper.Web.Infrastructure;

namespace Vouzamo.ScoreKeeper.Web.Controllers
{
    public class LeagueController : RepositoryController<League>
    {
        public LeagueController(DataContext context) : base(context)
        {
            
        }
    }
}
