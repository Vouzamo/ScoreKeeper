using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Common.Models.View
{
    public class Seasons
    {
        public League League { get; protected set; }
        public IPagedEnumerable<Season> PagedEnumerable { get; protected set; }

        public Seasons(League league, IPagedEnumerable<Season> pagedEnumerable)
        {
            League = league;
            PagedEnumerable = pagedEnumerable;
        }
    }
}
