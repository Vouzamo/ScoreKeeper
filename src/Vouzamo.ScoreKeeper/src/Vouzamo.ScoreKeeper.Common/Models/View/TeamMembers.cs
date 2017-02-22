using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Common.Models.View
{
    public class TeamMembers
    {
        public Team Team { get; protected set; }
        public IPagedEnumerable<TeamMember> PagedEnumerable { get; protected set; }

        public TeamMembers(Team team, IPagedEnumerable<TeamMember> pagedEnumerable)
        {
            Team = team;
            PagedEnumerable = pagedEnumerable;
        }
    }
}
