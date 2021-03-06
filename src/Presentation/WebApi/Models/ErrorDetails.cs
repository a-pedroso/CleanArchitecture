﻿namespace CleanArchitecture.WebApi.Models
{
    using System.Collections.Generic;
    using System.Text.Json;


    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
