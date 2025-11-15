using ExpTracker.Api.Extensions;
using ExpTracker.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.AddDbContext<ExpTrackerDbContext>(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("PG_CONNECTION_STRING");
    options.UseNpgsql(connectionString);
});


builder.AddServices();
builder.AddRepositories();

var app = builder.Build();

app.AddMiddlewares(builder.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();