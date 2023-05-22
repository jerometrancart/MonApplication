using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructure.Data;
using SelfieAWookies.Core.Selfies.Infrastructure.Repositories;
using SelfieAWookie.API.UI.ExtensionsMethods;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

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


//if you meet any occurence of an interface arg, take the class instead
//builder.Services.AddTransient<ISelfieRepository, DefaultSelfieRepository>();
builder.Services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();

var app = builder.Build();

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
