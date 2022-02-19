using Contoh.Microservice.Employee.Interfaces;
using Contoh.Microservice.Employee.Services;
using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWork;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


#region Repositories

var services = builder.Services;
services.AddTransient<IUnitOfWork, UnitOfWork>();
services.AddTransient<IUnitOfWork, UnitOfWork>();
services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddTransient<IEmployeeRepository, EmployeeRepository>();

#endregion

#region Services
//services.AddTransient<IEmployeeService, EmployeeService>();
services.AddTransient<IUnitService, UnitService>();
#endregion


services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(
  builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://host.docker.internal:49154";//"https://localhost:44352"; //
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});

builder.Host.ConfigureLogging(l => l.AddConsole());
builder.WebHost.ConfigureLogging(l => l.AddConsole());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
