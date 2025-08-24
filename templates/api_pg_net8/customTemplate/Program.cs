using Api.Api.Extensions;
using Api.Api.Middleware;
using Api.Infrastructure.Data.Contexts;
using FluentValidation;
using Mediator.Lite.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ItemsDbContext>(x => x
    .UseNpgsql(builder.Configuration.GetConnectionString(nameof(ItemsDbContext)))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediator(Assembly.GetExecutingAssembly());

builder.Services.AddProblemDetailsExtension();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ItemsDbContext>();

    context.Database.Migrate();
}

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();