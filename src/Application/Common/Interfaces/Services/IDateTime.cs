namespace CleanArchitecture.Application.Common.Interfaces.Services;

using System;

public interface IDateTime
{
    DateTime UtcNow { get; }
}
