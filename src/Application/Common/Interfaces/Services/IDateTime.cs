using System;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
