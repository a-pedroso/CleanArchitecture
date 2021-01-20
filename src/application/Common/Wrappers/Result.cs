using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Common.Wrappers
{
    public class Result
    {
        public bool IsSuccess { get; }
        public IEnumerable<string> Errors { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, IEnumerable<string> errors = null)
        {
            if((isSuccess && errors?.Count() > 0) || (!isSuccess && errors?.Count() <= 0))
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Fail(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result<T> Fail<T>(IEnumerable<string> errors)
        {
            return new Result<T>(default(T), false, errors);
        }

        //public static PagedResult<T> Fail<T>(IEnumerable<string> errors)
        //{
        //    return new PagedResult<T>(0, 0, 0, default(T), false, errors);
        //}

        public static Result Ok()
        {
            return new Result(true);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true);
        }

        public static PagedResult<T> Ok<T>(
            int pageNumber,
            int pageSize,
            int totalCount, 
            T value)
        {
            return new PagedResult<T>(pageNumber, pageSize, totalCount, value, true);
        }

        public static Result Combine(params Result[] results)
        {
            foreach(Result result in results)
            {
                if (result.IsFailure) 
                {
                    return result;
                }
            }

            return Ok();
        }
    }

    public class Result<T> : Result
    {
        private readonly T _data;

        public T Data
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _data;
            }
        }

        protected internal Result(T data, bool isSuccess, IEnumerable<string> errors = null)
            : base(isSuccess, errors)
        {
            _data = data;
        }
    }

    //public class PagedResult<T> : Result<T>
    //{
    //    public int PageNumber { get; } = 0;
    //    public int PageSize { get; } = 0;
    //    public int? TotalCount { get; } = 0;

    //    protected internal PagedResult(
    //        int pageNumber, 
    //        int pageSize, 
    //        int totalCount, 
    //        T value, 
    //        bool isSuccess, 
    //        IEnumerable<string> errors = null)
    //        : base(value, isSuccess, errors)
    //    {
    //        PageNumber = pageNumber;
    //        PageSize = pageSize;
    //        TotalCount = totalCount;
    //    }
    //}
}
