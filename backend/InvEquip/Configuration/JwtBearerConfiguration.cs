using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace InvEquip.Configuration
{
    public static class JwtBearerConfiguration
    {
        public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, byte[] key)
        {
            return builder.AddJwtBearer(jwtOptions =>
            {

                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                };
                jwtOptions.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["Jwt2"];
                        return Task.CompletedTask;
                    },
                    OnChallenge =  (context) =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";

                        }
                        //await HttpResponseWritingExtensions.WriteAsync(context.Response, $"Content-TEST: TEST{Environment.NewLine}");
                        //await context.Response.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ApiErrorResponse.ApiResponse.StatusCode424)));
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(new ValidationProblemDetails()
                        {
                            Status = 404,
                            Title = context.Error,
                            Detail = context.ErrorDescription,

                        }
                        ));

                    }
                };
            });
        }
    }
}
