using DebtManager.Api.Endpoints;
using DebtManager.Application.Features.Debts.Commands.AddDebt;
using DebtManager.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de Base de Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<DebtManager.Application.Interfaces.IAppDbContext, AppDbContext>();

// 2. Configuración de MediatR (CQRS)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AddDebtCommand).Assembly)
);

// 3. Documentación OpenAPI nativa de .NET 9
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            document.Info = new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Debt Manager API V1",
                Version = "v1",
                Description =
                    "API RESTful de Gestión de Sueldos y Deudas Personales (Clean Architecture + CQRS).",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Desarrollo",
                    Email = "dev@example.com"
                },
                License = new Microsoft.OpenApi.Models.OpenApiLicense { Name = "MIT" }
            };

            // Autenticación JWT (UI Button)
            document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();
            document.Components.SecuritySchemes.Add(
                "Bearer",
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Pega tu Token JWT aquí para autenticar las peticiones."
                }
            );

            document.SecurityRequirements.Add(
                new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );

            return System.Threading.Tasks.Task.CompletedTask;
        }
    );
});

var app = builder.Build();

// 4. Middlewares
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// 5. Mapeo de Minimal APIs (Endpoints)
app.MapUserEndpoints();
app.MapCashFlowEndpoints();
app.MapDebtEndpoints();
app.MapIncomeEndpoints();

app.Run();
