using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Api.Extensions;
using ProjectManagement.Api.Features;
using ProjectManagement.Api.Services;
using ProjectManagement.Api.Shared;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecurityTransformer>());
builder.Services.AddAuthenticationConfigured(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddScoped<IDataSeeder, AdminSeeder>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;

    if (args.Contains("--migrate", StringComparer.OrdinalIgnoreCase))
    {
        var dbContext = services.GetRequiredService<WriteDbContext>();
        await dbContext.Database.MigrateAsync();

        var seeder = services.GetRequiredService<IDataSeeder>();
        await seeder.SeedAsync();

        return;
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapFeaturesEndpoints();

app.Run();