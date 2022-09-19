using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace Core.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerUI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Your custom configuration
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Virtual Tour API", Version = "v1" });
                // JWT-token authentication by password
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Sử dụng api đăng nhập để lấy token hoặc lấy token từ FE, nhập token vào input value theo định dạng như sau: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }

}
