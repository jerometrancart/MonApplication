using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructure.Data;
using SelfieAWookies.Core.Selfies.Infrastructure.Repositories;
using SelfieAWookie.API.UI.ExtensionsMethods;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using SelfieAWookies.Core.Selfies.Infrastructure.Loggers;
using SelfieAWookie.API.UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
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

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    //options.Signin.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<SelfiesContext>();

builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddcustomSecurity(builder.Configuration);

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5030;
});


builder.Services.AddInjections();


var app = builder.Build();

//adds the provider to factory
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddProvider(new CustomLoggerProvider());

app.UseMiddleware<LogRequestMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
 

app.UseHttpsRedirection();
app.UseCors(SecurityMethods.DEFAULT_POLICY);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
