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
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            //services.AddScoped<IInternalTransferService, InternalTransferService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }

        public static IServiceCollection ListRepository(this IServiceCollection services)
        {
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            return services;
        }
    }
}
