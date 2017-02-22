using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Common.Models.View
{
    public class Teams
    {
        public League League { get; protected set; }
        public IPagedEnumerable<Team> PagedEnumerable { get; protected set; }

        public Teams(League league, IPagedEnumerable<Team> pagedEnumerable)
        {
            League = league;
            PagedEnumerable = pagedEnumerable;
        }
    }
}
