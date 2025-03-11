using ECom.Domain.Entities;
using ECom.Domain.Interfaces;
using ECom.Infrastructure.Data;
using ECom.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Infrastructure.AppDI
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "Default";
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(config.GetConnectionString(connectionString),
            sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();

            }),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>,GenericRepository<Product>>();
            services.AddScoped<IGeneric<Product>,GenericRepository<Product>>();

            return services;
        }
    }
}
