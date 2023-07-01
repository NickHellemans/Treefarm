using AP.MyTreeFarm.Application.Interfaces;
using AP.MyTreeFarm.Infrastructure.Contexts;
using AP.MyTreeFarm.Infrastructure.Repositories;
using AP.MyTreeFarm.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AP.MyTreeFarm.Infrastructure.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterDbContext();
            services.RegisterRepositories();
            return services;
        }
        public static IServiceCollection RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MyTreeFarmContext>(options =>
                        options.UseSqlServer("name=ConnectionStrings:MyTreeFarmDB"));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITreeTasksRepository, TreeTasksRepository>();
            services.AddScoped<IEmployeeRepository, EmployeesRepository>();
            services.AddScoped<ISiteRepository, SitesRepository>();
            services.AddScoped<ITreeRepository, TreeRepository>();
            services.AddScoped<IZoneRepository, ZonesRepository>();
            
            services.AddScoped<IUnitofWork, UnitofWork>();
            return services;
        }
    }
}
