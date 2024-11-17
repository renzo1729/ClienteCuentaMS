using AccountTransactionService.Application.Handlers;
using AccountTransactionService.Application.Mappings;
using AccountTransactionService.Application.Validators;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Response;
using AccountTransactionService.Infrastructure.Persistence.Context;
using AccountTransactionService.Infrastructure.Persistence.Repositories;
using AccountTransactionService.Infrastructure.Persistence.UnitOfWork;
using AccountTransactionService.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountTransactionService.Infrastructure.Configuration
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
            services.AddValidatorsFromAssemblyContaining<CreateAccountInputDtoValidator>();

            // Configuración de MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommandHandler).Assembly));

            // Registro de HttpClient para ClientService
            services.AddHttpClient<IClientService, ClientService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ClientService:BaseUrl"]);
            });

            // Registro de repositorios y UnitOfWork
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRecordRepository, TransactionRecordRepository>();
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
