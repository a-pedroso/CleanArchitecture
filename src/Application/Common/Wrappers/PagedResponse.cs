namespace CleanArchitecture.Application.Common.Wrappers
{
    using System.Collections.Generic;

    public class PagedResponse<T>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public IReadOnlyList<T> Data { get; }

        public PagedResponse(int pageNumber,
                             int pageSize,
                             int totalCount,
                             IReadOnlyList<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }
    }
}
