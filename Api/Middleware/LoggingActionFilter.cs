using Microsoft.AspNetCore.Mvc.Filters;

namespace Labo_Cts_backend.Api.Middleware
{
    public class LoggingActionFilter(ILogger<LoggingActionFilter> logger) : IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger = logger;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var arguments = context.ActionArguments;

            _logger.LogInformation("Action {ActionName} executing at {Time}", actionName, DateTime.Now);

            if (arguments.Any())
            {
                foreach (var arg in arguments)
                {
                    _logger.LogInformation("Argument: {Key} = {@Value}", arg.Key, arg.Value);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            _logger.LogInformation("Action {ActionName} executed at {Time}", actionName, DateTime.Now);

            if (context.Exception != null)
            {
                _logger.LogError("An exception occurred in {ActionName}: {Exception}", actionName, context.Exception.Message);
            }

            // Optionally log the result if available
            if (context.Result != null)
            {
                _logger.LogInformation("Action {ActionName} returned result: {@Result}", actionName, context.Result);
            }
        }
    }
}
