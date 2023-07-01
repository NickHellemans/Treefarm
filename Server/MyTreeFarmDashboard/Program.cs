using FluentValidation;
using FluentValidation.AspNetCore;
using RestSharp;
using AP.MyTreeFarm.Application.CQRS.Employees;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyTreeFarmDashboard.Services;
using AP.MyTreeFarm.Application.CQRS.Sites;
using AP.MyTreeFarm.Application.CQRS.Trees;
using MyTreeFarmDashboard.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    })
    .WithAccessToken(options =>
    {
        options.Audience = builder.Configuration["Auth0:Audience"];
        
        options.UseRefreshTokens = true;
        
        options.Events = new Auth0WebAppWithAccessTokenEvents
        {
            OnMissingRefreshToken = async (context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().WithRedirectUri("/").Build();
                await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            }
        };
    });


//Add access to HttpContext metadata (access token) in custom components or services
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(c => new RestClient("https://localhost:44352/api/v1/"));
builder.Services.AddSingleton<IRestService, RestService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateEmployeeDTO>, UpdateEmployeeDTOValidator>();
//builder.Services.AddScoped<IValidator<CreateTreeTaskDTO>, CreateTreeTaskDTOValidator>();
builder.Services.AddScoped<IValidator<CreateTreeDTO>, CreateTreeDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateTreeDTO>, UpdateTreeValidator>();
builder.Services.AddScoped<IValidator<CreateTaskVM>, CreateTaskVMDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateTaskVM>, UpdateTaskVMDTOValidator>();
builder.Services.AddScoped<IValidator<CreateZoneVM>, CreateZoneVMValidator>();
builder.Services.AddScoped<IValidator<UpdateZoneVM>, UpdateZoneVMDTOValidator>();
//builder.Services.AddScoped<IValidator<CreateZoneVM>, CreateZoneVMValidator>();
builder.Services.AddScoped<IValidator<CreateSiteDTO>, CreateSiteDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateSiteDTO>, UpdateSiteDTOValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
