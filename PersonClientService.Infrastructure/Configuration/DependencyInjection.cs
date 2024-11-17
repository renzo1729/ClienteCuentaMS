using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonClientService.Core.Domain.Interfaces;
using PersonClientService.Infrastructure.Persistence.Context;
using PersonClientService.Infrastructure.Persistence.Repositories;
using PersonClientService.Infrastructure.Persistence.unitofwork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PersonClientService.Core.Shared.Response;
using PersonClientService.Application.Mappings;
using PersonClientService.Application.Validators;
using PersonClientService.Application.Handlers;

namespace PersonClientService.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Registro de AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Registro de validadores de FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateClientInputValidator>();

            // Configuración de MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateClientCommandHandler).Assembly));


            // Registro de repositorios y UnitOfWork
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configuración de controladores y comportamiento de API
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        // Extraer errores de validación
                        var errors = context.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .Select(e => new
                            {
                                Field = e.Key,
                                Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                            });

                        // Crear la respuesta personalizada
                        var response = new ApiResponse<object>
                        {
                            Success = false,
                            Message = "Validation failed",
                            Data = errors
                        };

                        return new BadRequestObjectResult(response);
                    };
                });

            return services;
        }
    }
}
