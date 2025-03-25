using ECom.Application.Mapping;
using ECom.Application.Services.Implementations;
using ECom.Application.Services.Implementations.Authentication;
using ECom.Application.Services.Interfaces;
using ECom.Application.Services.Interfaces.Authentication;
using ECom.Application.Validations;
using ECom.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECom.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IValidationService,ValidationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            
            return services;
        }
    }
}
