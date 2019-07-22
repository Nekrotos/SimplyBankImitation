using Account.Domain.UseCases.Read.GetOne;
using Account.Domain.UseCases.Write.Create;
using Account.Domain.UseCases.Write.Deposit;
using Account.Domain.UseCases.Write.Transfer;
using Account.Domain.UseCases.Write.Withdraw;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Domain.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services
                .AddTransient<GetOneUseCase>()
                .AddTransient<CreateUseCase>()
                .AddTransient<DepositUseCase>()
                .AddTransient<WithdrawUseCase>()
                .AddTransient<TransferUseCase>();
            return services;
        }
    }
}
