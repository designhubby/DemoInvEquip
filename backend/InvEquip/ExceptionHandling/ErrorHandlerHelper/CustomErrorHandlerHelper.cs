using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InvEquip.ExceptionHandling.Exceptions;

//https://andrewlock.net/creating-a-custom-error-handler-middleware-function/

namespace InvEquip.ExceptionHandling.ErrorHandlerHelper
{
    public static class CustomErrorHandlerHelper
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }
        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);
        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex != null)
            {
                httpContext.Response.ContentType = "application/problem+json";
                var title = includeDetails ? "An error occured: " + ex.Message : "An error occured";
                var details = includeDetails ? ex.ToString() : null;

                ProblemDetails problem = new ();

                if (ex is BadHttpRequestException badHttpRequestException)
                {
                    problem.Title = "Invalid request";
                    problem.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode", BindingFlags.Public| BindingFlags.NonPublic | BindingFlags.Instance).GetValue(badHttpRequestException);
                    problem.Detail = badHttpRequestException.Message;
                }
                else if(ex is ExceptionEntityHasDependency exceptionEntityHasDependency)
                {
                    problem.Status = 500;
                    problem.Title = "EntityHasDependency";
                    problem.Detail = exceptionEntityHasDependency.Message;

                }
                else if(ex is ExceptionEntityNotExists exceptionEntityNotExists)
                {
                    problem.Status = 400;
                    problem.Title = "EntityNotExists";
                    problem.Detail = exceptionEntityNotExists.Message;
                }
                else
                {
                    problem.Status = 501;
                    problem.Title = title;
                    problem.Detail = details;
                }

                
                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                if(traceId != null)
                {
                    problem.Extensions["traceId"] = traceId;
                }
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}
