using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Common.Models.Domain;

namespace Vouzamo.ScoreKeeper.Web.Infrastructure
{
    public class DataContext : DbContext, IRepository<Guid>
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
