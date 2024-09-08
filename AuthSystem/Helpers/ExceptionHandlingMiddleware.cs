namespace AuthSystem.Helpers
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            string message;
            if (_env.IsDevelopment())
            {
                message = exception.Message;
            }
            else
            {
                message = "An error occurred. Please try again later.";
            }

            context.Response.StatusCode = statusCode;
            context.Response.Redirect($"/Error?message={message}");

            return Task.CompletedTask;
        }
    }

}
