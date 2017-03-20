using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Patterns.Specification.Interfaces;
using Vouzamo.ScoreKeeper.Common.Interfaces;
using Vouzamo.ScoreKeeper.Core.Specifications;
using Vouzamo.ScoreKeeper.Web.Extensions;

namespace Vouzamo.ScoreKeeper.Web.Controllers
{
    public class ApiController<T> : Controller where T : class, IEntity
    {
        protected const int ItemsPerPage = 9;

        protected IUnitOfWorkContext UnitOfWorkContext { get; }
        protected ISpecification<T> Specification { get; }
        
        public ApiController(IUnitOfWorkContext context)
        {
            UnitOfWorkContext = context;
            Specification = new EmptySpecification<T>();
        }

        public ApiController(IUnitOfWorkContext context, ISpecification<T> specification) : this(context)
        {
            Specification = specification;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List(int page = 1)
        {
            return await UnitOfWorkContext.Run(t => t.Query(Specification, page, ItemsPerPage)).HandleGet();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            return await UnitOfWorkContext.Run(t => t.Get<T>(id)).HandleGet();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            return await UnitOfWorkContext.Run(t => t.Run(c => c.Post(entity))).HandlePost();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromBody] T entity, Guid id)
        {
            return await UnitOfWorkContext.Run(t => t.Run(c => c.Put(id, entity))).HandlePut();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            return await UnitOfWorkContext.Run(t => t.Run(c => c.Delete<T>(id))).HandleDelete();
        }

        public ApiController<TAggregate> Aggregate<TAggregate>(ISpecification<TAggregate> specification) where TAggregate : class, IAggregate
        {
            return new ApiController<TAggregate>(UnitOfWorkContext, specification);
        }
    }
}
