using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Web.Infrastructure;

namespace Vouzamo.ScoreKeeper.Web.Controllers
{
    public abstract class RepositoryController<T> : Controller where T : class, IEntity<Guid>
    {
        protected DataContext Context { get; set; }

        protected RepositoryController(DataContext context)
        {
            Context = context;
        }

        public ActionResult List(int page, int resultsPerPage)
        {
            page = Math.Max(page, 1);
            resultsPerPage = Math.Max(resultsPerPage, 9);

            var skip = (page * resultsPerPage) - resultsPerPage;

            var results = Context.List<T>().Skip(skip).Take(resultsPerPage);

            return View(results);
        }

        public ActionResult Get(Guid id)
        {
            var result = Context.Get<T>(id);

            return View(result);
        }

        public ActionResult Post()
        {
            return View();
        }

        public ActionResult Post(T entity)
        {
            var result = Context.Post(entity);

            return View(result);
        }

        public ActionResult Put(Guid id)
        {
            var result = Context.Get<T>(id);

            return View(result);
        }

        public ActionResult Put(Guid id, T entity)
        {
            var result = Context.Put(entity, id);

            return View(result);
        }

        public ActionResult Delete(Guid id)
        {
            var result = Context.Get<T>(id);

            return View(result);
        }

        public ActionResult Delete(Guid id, T entity)
        {
            Context.Delete(entity);

            return View();
        }
    }
}
