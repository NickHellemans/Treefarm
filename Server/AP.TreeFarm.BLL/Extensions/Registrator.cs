using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace AP.MyTreeFarm.Application.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(c => new RestClient("https://dev-mlppzg45imwcpi8a.us.auth0.com/"));
            return services;
        }

    }
}
