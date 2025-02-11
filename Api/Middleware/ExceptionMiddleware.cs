using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Labo_Cts_backend.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid().ToString(); // Générer un TraceId unique
                _logger.LogError(ex, "An unexpected error occurred. TraceId: {TraceId}", traceId);
                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
        {
            context.Response.ContentType = "application/json";

            // Définir le code d'état et le message d'erreur par défaut
            var statusCode = HttpStatusCode.InternalServerError;
            var response = new ErrorResponse
            {
                TraceId = traceId,
                Message = "An unexpected error occurred.",
                Details = ex.Message
            };

            // Personnaliser pour des exceptions spécifiques
            switch (ex)
            {
                case ArgumentNullException argEx:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = "A required argument is missing.";
                    response.Details = argEx.Message;
                    break;

                case InvalidOperationException invOpEx:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = "The operation is not valid.";
                    response.Details = invOpEx.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    response.Message = "Access is denied.";
                    response.Details = "You do not have permission to access this resource.";
                    break;

                case KeyNotFoundException keyNotFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    response.Message = "The requested resource was not found.";
                    response.Details = keyNotFoundEx.Message;
                    break;

                case DbUpdateException dbEx:
                    statusCode = HttpStatusCode.Conflict;
                    response.Message = "A database error occurred.";
                    response.Details = dbEx.InnerException?.Message ?? dbEx.Message;
                    break;

                // Ajouter d'autres cas personnalisés pour les exceptions courantes de votre application
                default:
                    response.Details = "Internal Server Error - Check logs for more information.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            // Convertir l'objet réponse en JSON et écrire la réponse
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    // Modèle pour une réponse d'erreur avec TraceId
    public class ErrorResponse
    {
        public string TraceId { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
    }
}
