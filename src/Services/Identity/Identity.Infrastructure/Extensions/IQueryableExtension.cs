using Identity.Application.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<PagedResponse<T>> GetPagedAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var result = new PagedResponse<T>();
            result.PagedInfo.PageNumber = pageNumber;
            result.PagedInfo.PageSize = pageSize;
            result.PagedInfo.TotalRecords = query.Count();

            var pageCount = (double)result.PagedInfo.TotalRecords / pageSize;
            result.PagedInfo.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (pageNumber - 1) * pageSize;
            result.Data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
