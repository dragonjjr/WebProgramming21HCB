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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IDebtReminderService, DebtReminderService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IInternalTransferService, InternalTransferService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<INotificationService, NotificationService>();
            return services;
        }

        public static IServiceCollection ListRepository(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IDebtReminderRepository, DebtReminderRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInternalRepository, InternalTransferRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            return services;
        }
    }
}
