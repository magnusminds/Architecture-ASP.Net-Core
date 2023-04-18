namespace Architecture.WebAPI.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UseExceptionHandlerMiddleware>();
        }
    }
}
