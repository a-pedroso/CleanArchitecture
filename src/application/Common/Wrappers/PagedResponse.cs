using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? TotalCount { get; set; }
            

        // Sucesss
        private PagedResponse(
            T data, 
            int pageNumber, 
            int pageSize, 
            int? totalCount = null, 
            string message = null)
        {
            Succeeded = true;
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Message = message;
            Errors = null;
        }

        // Failure
        private PagedResponse(
            int pageNumber,
            int pageSize,
            int? totalCount = null,
            string message = null,
            IEnumerable<string> errors = null)
        {
            Succeeded = false;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Message = message;
            Errors = errors;
        }

        public static PagedResponse<T> Success(
            T data, int pageNumber, int pageSize, int? totalCount = null, string message = null)
        {
            return new PagedResponse<T>(data, pageNumber, pageSize, totalCount, message);
        }

        public static PagedResponse<T> Failure(
            int pageNumber, int pageSize, int? totalCount = null, string message = null, IEnumerable<string> errors = null)
        {
            return new PagedResponse<T>(pageNumber, pageSize, totalCount, message, errors);
        }

    }
}
