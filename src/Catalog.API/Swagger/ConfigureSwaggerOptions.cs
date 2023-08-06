using Catalog.API.DTOs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Catalog.API.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            ConfigureSchemas(options);
            ConfigureVersions(options);
        }

        private static void ConfigureSchemas(SwaggerGenOptions options)
        {
            options.UseOneOfForPolymorphism();
            options.SelectSubTypesUsing(baseType =>
            {
                if (baseType == typeof(ICategoryDto))
                {
                    return new[]
                    {
                        typeof(CategoryWithProductsDto),
                        typeof(CategoryWithoutProductsDto)
                    };
                }
                return Enumerable.Empty<Type>();
            });
        }

        private void ConfigureVersions(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = Assembly.GetCallingAssembly().GetName().Name,
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated.";

            return info;
        }
    }
}
