using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Errors";
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
