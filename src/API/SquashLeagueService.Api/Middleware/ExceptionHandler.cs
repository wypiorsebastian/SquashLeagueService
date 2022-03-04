using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using SquashLeagueService.Application.Common.Exceptions;
using SquashLeagueService.Domain.Exceptions;

namespace SquashLeagueService.Api.Middleware;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
                
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;
            string? errorCode = string.Empty;

            try
            {
                errorCode = (string)exception.Data["error_code"];
            }
            catch
            {
                // ignored
            }

            switch (exception)
            {
                case UserAlreadyExistsException userAlreadyExistsException:
                    httpStatusCode = HttpStatusCode.UnprocessableEntity;
                    result = userAlreadyExistsException.Message;
                    break;
                case UserAuthenticationException userAuthenticationException:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    result = userAuthenticationException.Message;
                    break;
                case { } ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result != string.Empty)
            {
                result = JsonSerializer.Serialize(new {error = exception.Message, errorCode = errorCode});
            }

            return context.Response.WriteAsync(result);
        }
}