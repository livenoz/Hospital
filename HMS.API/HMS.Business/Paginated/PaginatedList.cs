using HMS.Business.Interfaces.Paginated;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HMS.Business.Paginated
{
    [JsonObject]
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Clear();
            AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public IEnumerable<T> Items => this.Select(c => c);

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
