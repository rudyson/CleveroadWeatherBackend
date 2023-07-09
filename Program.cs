using CleveroadWeatherBackend.Models.Configuration;
using CleveroadWeatherBackend.Repositories;
using Microsoft.OpenApi.Models;

#region API builder configuraion

var builder = WebApplication.CreateBuilder(args);

// Using Newtonsoft.Json instead of System.Text.Json
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Options injection
builder.Services.ConfigureOptions<OpenWeatherMapConfigureOptions>();

// Lowercase URLs for routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Repository
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

// CORS policy : Allow all
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowAll", p =>
        {
            p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

// Swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Weather API",
        Description = "An API to view current weather and forecast",
        Contact = new OpenApiContact
        {
            Name = "Ruslan Diadiushkin",
            Email = "contact@xnrudyson.anonaddy.me",
            Url = new Uri("https://www.linkedin.com/in/rudyson")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License (X11)",
            Url = new Uri("https://github.com/rudyson/CleveroadWeatherBackend/blob/master/LICENSE")
        }
    });
});
#endregion

#region API application configuration

var app = builder.Build();

// Enabling CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion