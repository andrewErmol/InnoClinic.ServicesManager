using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServicesManager.API.Extensions;
using ServicesManager.Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ServicesController).Assembly);

builder.Services.ConfigureCors();

builder.Services.ConfigurePostgreSqlContext(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication("Bearer")
         .AddJwtBearer("Bearer", options =>
         {
             options.Authority = "https://localhost:7130";

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false
             };
         });

builder.Services.AddAuthentication();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();