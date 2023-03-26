using Common.Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Shared.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class, new()
        {
            var result = new PagedResult<T>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalRecordCount = query.Count();

            double pageCount = (double)result.TotalRecordCount / pageSize;
            result.TotalPageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
        public static Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class, new() => Task.FromResult(GetPaged(query, page, pageSize));

    }
}
