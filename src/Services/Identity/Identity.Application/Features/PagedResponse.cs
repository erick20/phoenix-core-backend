using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features
{
    public class PagedResponse<T> where T : notnull
    {
        public PagedInfo PagedInfo { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }

    public class PagedInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }
    }
}
