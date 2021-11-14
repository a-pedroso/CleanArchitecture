namespace CleanArchitecture.Infrastructure.Shared.Services;

using CleanArchitecture.Application.Common.Interfaces.Services;
using System;

public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
