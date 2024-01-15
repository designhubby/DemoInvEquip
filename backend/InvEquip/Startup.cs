using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Mapper;
using InvEquip.Data;
using InvEquip.Data.Repository;
using InvEquip.Logic.Service;
using InvEquip.ExceptionHandling.Extensions;
using InvEquip.ExceptionHandling.ErrorHandlerHelper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using InvEquip.ExceptionHandling.Exceptions;
using Microsoft.EntityFrameworkCore;
using InvEquip.Logic.Service.Helper;
using InvEquip.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using InvEquip.Data.Authentication;
using System.IO;

namespace InvEquip
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureAppConfiguration(IConfigurationBuilder builder)
        {
            builder
                .AddEnvironmentVariables();
        }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig")).Configure<ConnectionStrings>(Configuration.GetSection(ConnectionStrings.ConnectionStringName));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(new[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200", "http://127.0.0.1:3000", "https://invequipfe01.z27.web.core.windows.net" })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(cookieOptions =>
            {
                cookieOptions.Cookie.Name = "Jwt2";
                cookieOptions.Cookie.Path = "/";
                cookieOptions.Cookie.HttpOnly = true;
                cookieOptions.Cookie.IsEssential = true;
            })
            .AddJwtBearerConfiguration(
                Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"])
                );

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                //Password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;

            });

            services.AddProblemDetails(ConfigureProblemDetails);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InvEquip", Version = "v1" });
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<ApplicationDbContext>();
            services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
            services.AddScoped<IPersonDeviceService, PersonDeviceService>();
            services.AddScoped<IApplicationPersonService, ApplicationService>();
            services.AddScoped<IApplicationDepartmentService, ApplicationService>();
            services.AddScoped<IApplicationPersonDeviceService, ApplicationService>();
            services.AddScoped<IApplicationDeviceService, ApplicationService>();
            services.AddScoped<IApplicationHwModelService, ApplicationService>();
            services.AddScoped<IApplicationContractService, ApplicationService>();
            services.AddScoped<IApplicationVendorService, ApplicationService>();
            services.AddScoped<IApplicationDeviceTypeService, ApplicationService>();
            services.AddScoped<IApplicationRoleService, ApplicationService>();
            //services.AddScoped<IApplicationAuthService, ApplicationService>();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IJwtService, JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler(err => err.UseCustomErrors(env));
                
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvEquip v1"));
                IdentityModelEventSource.ShowPII = true;
            }
            app.UseProblemDetails();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void ConfigureProblemDetails(ProblemDetailsOptions options)
        {
            options.IncludeExceptionDetails = (ctx, ex) =>
            {
                var environment = ctx.RequestServices.GetRequiredService<IWebHostEnvironment>();
                return true;
                //return environment.IsDevelopment();
            };
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<ExceptionEntityHasDependency>(StatusCodes.Status424FailedDependency);
            options.MapToStatusCode<ExceptionEntityNotExists>(StatusCodes.Status404NotFound);
            options.MapToStatusCode<BadHttpRequestException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            options.MapToStatusCode<NotSupportedException>(StatusCodes.Status500InternalServerError);
            options.MapToStatusCode<DbUpdateException>(StatusCodes.Status500InternalServerError);
            options.MapToStatusCode<InvalidOperationException>(StatusCodes.Status500InternalServerError);
        }
    }
}
