using Api.Application.Commands.AddItem;
using Api.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ItemsDbContext>(x => x
    .UseNpgsql(builder.Configuration.GetConnectionString(nameof(ItemsDbContext)))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(AddItemCommand)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ItemsDbContext>();

    context.Database.Migrate();
}

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();