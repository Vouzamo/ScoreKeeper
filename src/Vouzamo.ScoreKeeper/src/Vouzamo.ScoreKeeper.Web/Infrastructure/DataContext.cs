using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Web.Infrastructure
{
    public class DataContext : DbContext, IRepository<Guid>
    {
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Handicap> Handicaps { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public IQueryable<T> List<T>() where T : class, IEntity<Guid>
        {
            return Set<T>();
        }

        public T Get<T>(Guid id) where T : class, IEntity<Guid>
        {
            return List<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Post<T>(T entity) where T : class, IEntity<Guid>
        {
            entity.Id = Guid.NewGuid();

            Entry(entity).State = EntityState.Added;

            return entity;
        }

        public T Put<T>(T entity, Guid id) where T : class, IEntity<Guid>
        {
            entity.Id = id;

            Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void Delete<T>(T entity) where T : class, IEntity<Guid>
        {
            Entry(entity).State = EntityState.Deleted;
        }
    }
}
