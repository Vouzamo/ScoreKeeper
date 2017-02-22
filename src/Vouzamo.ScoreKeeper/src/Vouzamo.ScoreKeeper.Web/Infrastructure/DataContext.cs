using Microsoft.EntityFrameworkCore;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Web.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fixture>();
            modelBuilder.Entity<Game>();
            modelBuilder.Entity<Individual>();
            modelBuilder.Entity<League>();
            modelBuilder.Entity<Rack>();
            modelBuilder.Entity<Season>();
            modelBuilder.Entity<Team>();
            modelBuilder.Entity<TeamMember>();

            base.OnModelCreating(modelBuilder);
        }
    }
}

