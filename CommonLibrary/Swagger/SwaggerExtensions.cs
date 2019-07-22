using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CommonLibrary.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocs(
            this IServiceCollection services,
            IConfiguration configuration,
            List<string> filePaths
        )
        {
            var options = new SwaggerOptions();
            configuration.GetSection("Swagger").Bind(options);

            if (!options.Enabled)
                return services;

            return services.AddSwaggerGen(genOptions =>
            {
                genOptions
                    .SwaggerDoc(
                        options.Name,
                        new Info
                        {
                            Title = options.Title,
                            Version = options.Version
                        });

                if (options.IncludeSecurity)
                    genOptions
                        .AddSecurityDefinition(
                            "Bearer",
                            new ApiKeyScheme
                            {
                                Description =
                                    "JWT Authorization header using the Bearer scheme. " +
                                    "Example: \"Authorization: Bearer {token}\"",
                                Name = "Authorization",
                                In = "header",
                                Type = "apiKey"
                            });
                genOptions
                    .OperationFilter<FilesUploadOperation>();

                genOptions
                    .DescribeAllEnumsAsStrings();

                genOptions
                    .CustomSchemaIds(type => type.FullName);

                filePaths?.ForEach(fp =>
                {
                    if (File.Exists(fp))
                        genOptions.IncludeXmlComments(fp);
                });
            });
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = new SwaggerOptions();
            builder.ApplicationServices.GetService<IConfiguration>().GetSection("Swagger").Bind(options);

            if (!options.Enabled)
                return builder;

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix)
                ? "swagger"
                : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c => { c.RouteTemplate = routePrefix + "/{documentName}/swagger.json"; });

            return options.ReDocEnabled
                ? builder.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SpecUrl = "v1/swagger.json";
                })
                : builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    c.RoutePrefix = routePrefix;
                });
        }
    }
}