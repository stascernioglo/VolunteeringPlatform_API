using VolunteeringPlatform.API.Infrastructure.Middlewares;

namespace VolunteeringPlatform.API.Infrastructure.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlingMiddleware>();

        public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app) => app.UseMiddleware<DbTransactionMiddleware>();
    }
}
