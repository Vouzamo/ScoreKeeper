using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vouzamo.ScoreKeeper.Common.Interfaces
{
    public interface IError
    {
        int Code { get; }
        string Message { get; }
    }

    public class Error : IError
    {
        public int Code { get; protected set; }
        public string Message { get; protected set; }

        public Error(string message, int code = 0)
        {
            Code = code;
            Message = message;
        }
    }

    public interface IResponse
    {
        bool Success { get; }
        IEnumerable<IError> Errors { get; }
    }

    public interface IObjectResponse<out T> : IResponse
    {
        T Object { get; }
    }

    public class Response : IResponse
    {
        public bool Success { get; protected set; }
        protected IList<IError> ErrorList { get; set; }
        public IEnumerable<IError> Errors => ErrorList;

        public Response(bool success)
        {
            Success = success;
            ErrorList = new List<IError>();
        }

        public void AddError(IError error)
        {
            ErrorList.Add(error);
        }

        public void ClearErrors()
        {
            ErrorList.Clear();
        }
    }

    public class ObjectResponse<T> : Response, IObjectResponse<T>
    {
        public T Object { get; protected set; }

        public ObjectResponse(bool success, T obj) : base(success)
        {
            Object = obj;
        }
    }

    public interface IPagination
    {
        int CurrentPage { get; }
        int TotalCount { get; }
        int ItemsPerPage { get; }
        int TotalPages { get; }
        bool HasNextPage { get; }
        bool HasPreviousPage { get; }
    }

    public interface IPagedEnumerable<out T> : IPagination
    {
        IEnumerable<T> Enumerable { get; }
    }

    public class PagedEnumerable<T> : IPagedEnumerable<T>
    {
        public IEnumerable<T> Enumerable { get; protected set; }
        public int CurrentPage { get; protected set; }
        public int TotalCount { get; protected set; }
        public int ItemsPerPage { get; protected set; }

        public int TotalPages
        {
            get
            {
                var totalPages = TotalCount / ItemsPerPage;

                if (TotalCount % ItemsPerPage > 0)
                {
                    totalPages++;
                }

                return totalPages;
            }
        }

        public bool HasNextPage => TotalPages > CurrentPage;
        public bool HasPreviousPage => CurrentPage > 1;

        public PagedEnumerable(IEnumerable<T> enumerable, int currentPage, int totalCount, int itemsPerPage)
        {
            Enumerable = enumerable;
            CurrentPage = currentPage;
            TotalCount = totalCount;
            ItemsPerPage = itemsPerPage;
        }
    }

    public static class Helpers
    {
        public static Error ToError(this Exception exception, int code = 0)
        {
            return new Error(exception.Message, code);
        }

        public static IPagedEnumerable<T> ToPagedEnumerable<T>(this IQueryable<T> queryable, int page, int itemsPerPage)
        {
            page = Math.Max(page, 1);
            itemsPerPage = Math.Max(itemsPerPage, 1);

            var totalCount = queryable.Count();
            var skip = (page * itemsPerPage) - itemsPerPage;
            var take = itemsPerPage;

            var enumerable = queryable.Skip(skip).Take(take).ToList();

            return new PagedEnumerable<T>(enumerable, page, totalCount, itemsPerPage);
        }

        public static async Task<IPagedEnumerable<T>> ToPagedEnumerableAsync<T>(this IQueryable<T> queryable, int page, int itemsPerPage)
        {
            page = Math.Max(page, 1);
            itemsPerPage = Math.Max(itemsPerPage, 1);

            var totalCount = queryable.Count();
            var skip = (page * itemsPerPage) - itemsPerPage;
            var take = itemsPerPage;

            var enumerable = await queryable.Skip(skip).Take(take).ToListAsync();

            return new PagedEnumerable<T>(enumerable, page, totalCount, itemsPerPage);
        }
    }
}
