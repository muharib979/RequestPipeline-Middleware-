using System.Net;
using System.Text.Json;

namespace RequestPipelineDemo.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                var response = new
                {
                    Message = "সার্ভারে একটি সমস্যা হয়েছে।",
                    Error = ex.Message
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            // যদি ModelState Invalid হয়, তাহলে ধরার উপায়:
            if (!context.Response.HasStarted && context.Response.StatusCode == 400 && context.Items.ContainsKey("ModelStateErrors"))
            {
                var errors = context.Items["ModelStateErrors"];
                context.Response.ContentType = "application/json";
                var validationResponse = new
                {
                    Message = "তথ্যগুলো সঠিক নয়",
                    Errors = errors
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
            }
        }
    }
}
