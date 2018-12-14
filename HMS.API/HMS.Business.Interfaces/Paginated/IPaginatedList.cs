using System.Collections.Generic;

namespace HMS.Business.Interfaces.Paginated
{
    public interface IPaginatedList<T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
        int TotalItems { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        IEnumerable<T> Items { get; }
    }
}
