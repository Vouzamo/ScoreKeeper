using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vouzamo.ScoreKeeper.Common.Interfaces;

namespace Vouzamo.ScoreKeeper.Web.Extensions
{
    public static class ResponseExtensions
    {
        public static async Task<IActionResult> HandleGet<TResponse>(this Task<TResponse> response)
        {
            try
            {
                var result = await response;

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public static async Task<TResult> AsSafe<TResult, T>(this Task<T> task, Func<T,TResult> success, Func<Exception, TResult> error) where TResult : IActionResult
        {
            try
            {
                var result = await task;

                return success.Invoke(result);
            }
            catch (Exception ex)
            {
                return error.Invoke(ex);
            }
        }

        public static async Task<IActionResult> HandlePost<TResponse>(this Task<TResponse> response) where TResponse : IEntity
        {
            try
            {
                var result = await response;

                return new CreatedResult(result.Id.ToString(), result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public static async Task<IActionResult> HandlePut(this Task response)
        {
            try
            {
                await response;

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public static async Task<IActionResult> HandleDelete(this Task response)
        {
            try
            {
                await response;

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
