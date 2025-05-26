namespace RequestPipelineDemo.Middleware
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Response এর শেষে হেডার অ্যাড করবো
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey("X-Powered-By"))
                {
                    context.Response.Headers.Add("X-Powered-By", "RequestPipelineDemo");
                }
                return Task.CompletedTask;
            });

            await _next(context); // পরবর্তী middleware/endpoint এ পাঠানো
        }
    }
}