using System.Collections.Generic;

namespace CleanArchitecture.Application.Common.Wrappers
{
    public class Response<T>
    {
        internal Response()
        {
        }

        // Success
        private Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            Errors = null;
        }

        // Failure
        private Response(string message, IEnumerable<string> errors = null)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }


        public static Response<T> Success(T data, string message = null)
        {
            return new Response<T>(data, message);
        }

        public static Response<T> Failure(string message = null, IEnumerable<string> errors = null)
        {
            return new Response<T>(message, errors);
        }
    }
}
