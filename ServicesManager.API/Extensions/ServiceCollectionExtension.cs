using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServicesManager.Domain.IRepositories;
using ServicesManager.Persistence;
using ServicesManager.Persistence.Repositories;
using ServicesManager.Presentation.Validators;
using ServicesManager.Services.Abstractions.IServices;
using ServicesManager.Services.Services;

namespace ServicesManager.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(optionss =>
            {
                optionss.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigurePostgreSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ServicesDbContext>(opts =>
                opts.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"), b =>
                b.MigrationsAssembly("ServicesManager.Persistence")));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ServiceForRequestValidator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
