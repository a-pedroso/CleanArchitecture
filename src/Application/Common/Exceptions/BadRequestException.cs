namespace CleanArchitecture.Application.Common.Exceptions
{
    using System;
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
