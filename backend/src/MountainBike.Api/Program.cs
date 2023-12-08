using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MountainBike.Services.Settings;
using MountainBike.Services.Repositories;
using MountainBike.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
var mongoDBsettings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
var reactClientSettings = builder.Configuration.GetSection(nameof(ReactClientSettings)).Get<ReactClientSettings>();

builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    return new MongoClient(mongoDBsettings?.ConnectionString);
});

builder.Services.AddSingleton<IBikeRepository, MongoDBBikeRepository>();
builder.Services.AddSingleton<IRiderRepository, MongoDBRiderRepository>();

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IRiderService, RiderService>();

builder.Services.AddControllers(option =>
{
    option.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowOrigin",
            builder => builder.WithOrigins(reactClientSettings!.ConnectionString) // Add your React app's origin
            .AllowAnyMethod()
            .AllowAnyHeader());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddMongoDb(mongoDBsettings?.ConnectionString ?? "",
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async (context, report) =>
    {
        var result = JsonSerializer.Serialize(
            new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.ToString(),
                    exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "No exception message",
                    duration = entry.Value.Duration.ToString()
                })
            }
        );

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = (_) => false
});

app.Run();
