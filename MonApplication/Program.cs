using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Set the connectionString in appsettings.json first
var connectionString = builder.Configuration.GetConnectionString("SelfiesDatabase");
// add context in the dependencies, via efcore
builder.Services.AddDbContext<SelfiesContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions => { });
});

//if you meet any occurence of an interface arg, take the class instead
builder.Services.AddTransient<ISelfieRepository, DefaultSelfieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
