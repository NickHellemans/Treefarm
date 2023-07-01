using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;
using AP.MyTreeFarm.Application.CQRS.Employees;
using AP.MyTreeFarm.Application.CQRS.TreeTasks;
using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Trees;
using AP.MyTreeFarm.Application.CQRS.Zones;
using AP.MyTreeFarm.Application.Extensions;
using AP.MyTreeFarm.Infrastructure.Extensions;
using AP.MyTreeFarm.WebAPI.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AP.MyTreeFarm.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterInfrastructure();
            services.RegisterApplication();
            services.AddControllers();

            services.AddMvc();
            var domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
                
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Open Id" },
                                { "read:all", "Read access" },
                                { "edit", "Edit access" },
                            },
                            TokenUrl = new Uri("https://dev-mlppzg45imwcpi8a.us.auth0.com/oauth/token"),
                            AuthorizationUrl = new Uri(Configuration["Auth0:DomainFull"] + "authorize?audience=" + Configuration["Auth0:Audience"])
                        }
                    }
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "oauth2" }
                    }
                });

            });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                  
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            
            
            services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidator>();
            services.AddScoped<IValidator<CreateTreeTaskDTO>, CreateTreeTaskDTOAdvancedValidator>();
            services.AddScoped<IValidator<UpdateTreeTaskDTO>, UpdateTreeTaskDTOAdvancedValidator>();
            
            //Middleware for OAuth
            services
                .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EmployeeAccess", policy => policy.RequireClaim("permissions", "read:all"));
                options.AddPolicy("AdminAccess", policy => policy.RequireClaim("permissions", "edit"));
            });
            
            services.AddScoped<IValidator<CreateSiteDTO>, CreateSiteDTOValidator>();
            services.AddScoped<IValidator<UpdateEmployeeDTO>, UpdateEmployeeDTOValidator>();
            services.AddScoped<IValidator<CreateTreeDTO>, CreateTreeDTOValidator>();
            services.AddScoped<IValidator<CreateZoneDTO>, CreateZoneDTOAdvancedValidator>();
            services.AddScoped<IValidator<UpdateSiteDTO>, UpdateSiteDTOValidator>();
            services.AddScoped<IValidator<UpdateZoneDTO>, UpdateZoneDTOAdvancedValidator>();
            services.AddScoped<IValidator<UpdateTreeDTO>, UpdateTreeValidator>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1");
                    c.OAuthClientId(Configuration["Auth0:ClientIdTest"]);
                });
            }

            app.UseHttpsRedirection();

            app.UseErrorHandlingMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
