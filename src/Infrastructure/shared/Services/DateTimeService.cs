using CleanArchitecture.Application.Common.Interfaces.Services;
using System;

namespace CleanArchitecture.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
