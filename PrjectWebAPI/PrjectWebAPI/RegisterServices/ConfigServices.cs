using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;

namespace PrjectWebAPI.RegisterServices
{
    public static class ConfigServices
    {
        public static IServiceCollection ListServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            //services.AddScoped<IInternalTransferService, InternalTransferService>();
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }

        public static IServiceCollection ListRepository(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
