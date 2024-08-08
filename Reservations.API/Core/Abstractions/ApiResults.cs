using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Reservations.API.Core.Abstractions
{
    public static class ApiResults
    {
        public static ActionResult Problem(Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException();

            var problemDetails = new ProblemDetails
            {
                Title = GetTitle(result.Error.Type),
                Detail = GetDetail(result.Error),
                Type = GetType(result.Error.Type),
                Status = GetStatusCode(result.Error.Type),
                Extensions = { { "errors", new[] { result.Error } } }
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }

        private static int? GetStatusCode(ErrorType type) =>
            type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };


        private static string? GetType(ErrorType type)
        {
            return nameof(type);
        }

        private static string? GetDetail(Error error)
        {
            return error.Message;
        }

        private static string? GetTitle(ErrorType type) =>
            type switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Internal Server Error"
            };
    }
}
