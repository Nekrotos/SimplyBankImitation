using System;
using Account.Domain.DomainModels.Account;
using Account.Domain.InfrastructureInterfaces;
using Account.Infrastructure.MsSql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure.MsSql.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = Environment
                .GetEnvironmentVariable("DEFAULT_CONNECTION");

            if (string.IsNullOrWhiteSpace(connection))
                connection = configuration
                    .GetConnectionString("DefaultConnection");

            services
                .AddDbContext<AccountContext>(options =>
                    SqlServerDbContextOptionsExtensions.UseSqlServer(
                        options,
                        connection))
                .AddTransient<IRead<AccountDomain>, AccountRepository>()
                .AddTransient<IWrite<AccountDomain>, AccountRepository>();

            var context = services
                .BuildServiceProvider()
                .GetService<AccountContext>();

            var isExist = ((RelationalDatabaseCreator) context
                    .Database
                    .GetService<IDatabaseCreator>())
                .Exists();

            if (isExist == false)
                context.Database.Migrate();

            return services;
        }
    }
}