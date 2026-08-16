
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelerWay.Common.Exceptions;

namespace TravelerWay.Api.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken ct)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (exception == null) return false;
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started; cannot write error response.");
                return false;
            }

            var (statusCode, title, detail) = exception switch
            {
                NotificationException ex => (ex.StatusCode, ex.Name,ex.Details ),
                DuffelException ex => (ex.StatusCode, ex.Name,ex.Details ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Server Error",
                    "An unexpected internal error occurred."
                )
            };

            _logger.LogError(exception, "Request failed: {Title} | Details: {Detail}", title, detail);

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request?.Path
            }, ct).ConfigureAwait(false);

            return true;
        }
    }
}
