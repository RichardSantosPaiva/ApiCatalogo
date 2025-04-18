using System.Net;
using ApiCatalogo.Model;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiCatalogo.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorDetails = new ErrorDetails 
                        {
                            StatusCode = context.Response.StatusCode.ToString(),
                            Message = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace
                        };

                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}
