using AP.MyTreeFarm.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AP.MyTreeFarm.WebAPI.Extensions;

public static class Registrator
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
