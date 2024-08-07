using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TypesOfSportingEvents.API.Core.Abstractions
{
    public static class ApiResults
    {
        public static ActionResult Problem(Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException();

            var problemDetails = new ProblemDetails
            {
                Title = GetTitle(result.Error),
                Detail = GetDetail(result.Error),
                Type = GetType(result.Error),
                Status = GetStatusCode(result.Error),
                Extensions = { { "errors", GetErrors(result) } }
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }

        private static IDictionary<string, object?>? GetErrors(Result result)
        {
            return new Dictionary<string, object?>
            {
                { result.Error.Code, result.Error.Message }
            };
        }

        private static int? GetStatusCode(Error error)
        {
            return (int)HttpStatusCode.BadRequest;
        }

        private static string? GetType(Error error)
        {
            return nameof(error);
        }

        private static string? GetDetail(Error error)
        {
            return error.Message;
        }

        private static string? GetTitle(Error error)
        {
            return error.Code;
        }
    }
}
