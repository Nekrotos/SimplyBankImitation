using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CommonLibrary.Swagger
{
    public class FilesUploadOperation 
        : IOperationFilter
    {
        public void Apply(
            Operation operation, 
            OperationFilterContext context)
        {
            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                return;

            var apiDescription = context.ApiDescription;
            var parameters = context.ApiDescription.ParameterDescriptions
                .Where(n => n.Type == typeof(IFormFileCollection) || n.Type == typeof(IFormFile))
                .ToList();
            if (!parameters.Any())
                return;

            operation.Consumes.Add("multipart/form-data");
            foreach (var fileParameter in parameters)
            {
                var parameter = operation.Parameters.Single(n => n.Name == fileParameter.Name);
                operation.Parameters.Remove(parameter);
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = parameter.Name,
                    In = "formData",
                    Description = parameter.Description,
                    Required = parameter.Required,
                    Type = "file"
                });
            }
        }
    }
}