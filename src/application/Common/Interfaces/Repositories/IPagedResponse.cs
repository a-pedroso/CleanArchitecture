using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Interfaces.Repositories
{
    public interface IPagedResponse<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
