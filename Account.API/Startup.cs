using Account.Domain.DomainModels.SeedOfWork;
using Account.Domain.Extensions;
using Account.Infrastructure.MsSql.Extensions;
using CommonLibrary.Middlewares;
using CommonLibrary.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMediatR(
                    typeof(Startup).Assembly,
                    typeof(IDomainAssembly).Assembly
                )
                .AddDomainServices()
                .AddInfrastructureServices(Configuration)
                .AddSwaggerDocs(Configuration, null)
                .AddCors()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseMiddleware<CorrelationTokenMiddleware>()
                .UseMiddleware<InternalServerErrorMiddleware>()
                .UseSwaggerDocs()
                .UseCors(builder => builder.AllowAnyOrigin())
                .UseHttpsRedirection()
                .UseMvc();
        }
    }
}
