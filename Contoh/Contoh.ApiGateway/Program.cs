using Contoh.ApiGateway.Helpers;
using Contoh.ApiGateway.Services;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    //services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    //// configure DI for application services
    //services.AddScoped<IUserService, UserService>();

    services.AddEndpointsApiExplorer();

    services.AddOcelot();
    services.AddSwaggerGen();

    //builder.Configuration.AddJsonFile("ocelot.json");
}



//var authenticationProviderKey = "IdentityApiKey";

//// NUGET - Microsoft.AspNetCore.Authentication.JwtBearer
//builder.Services.AddAuthentication()
// .AddJwtBearer(authenticationProviderKey, x =>
// {
//     x.Authority = "http://host.docker.internal:49154"; // IDENTITY SERVER URL
//     x.RequireHttpsMetadata = false;
//     x.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateAudience = false
//     };
// });


builder.Logging.AddConsole();

//builder.Services.AddOcelot();
builder.Host.ConfigureAppConfiguration(config => config.AddJsonFile("ocelot.json"));
builder.WebHost.ConfigureAppConfiguration(config => config.AddJsonFile("ocelot.json"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseOcelot().Wait();

app.Run();
