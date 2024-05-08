using MongoDB.Driver;
using OzPerksApi.Interfaces;
using OzPerksApi.Models;
using OzPerksApi.Services;


var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional:true).Build();


// Add services to the container.

// Configure MongoDb client and database.

var ozPerksHubDbSettings = new OzPerksHubDbSettings();
builder.Configuration.GetSection("OzPerksHubDb").Bind(ozPerksHubDbSettings);
var client = new MongoClient(ozPerksHubDbSettings.ConnectionString);
var database = client.GetDatabase(ozPerksHubDbSettings.DatabaseName);

// Register MongoDb client and database
builder.Services.AddSingleton<IMongoClient>(client);
builder.Services.AddSingleton<IMongoDatabase>(database);

// Register repositories with collection name
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

// Register repositories services with specific collection name
builder.Services.AddScoped<IRepositoryService<User>>(s => new RepositoryService<User>(s.GetRequiredService<IMongoDatabase>(), "users"));  //User collection

//...... add others


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "api/{controller}/{id}");

app.Run();
