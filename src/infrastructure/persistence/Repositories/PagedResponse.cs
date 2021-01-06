using CleanArchitecture.Application.Common.Interfaces.Repositories;
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class PagedResponse<T> : IPagedResponse<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
