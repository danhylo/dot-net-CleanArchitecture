var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços para Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiSample01", Version = "v1" });
});

// Adiciona suporte a controllers com validação customizada
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiSample01.Api.Filters.ModelValidationFilter>();
});

// Configura comportamento da API para usar validação customizada
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Registra o repositório
builder.Services.AddScoped<ApiSample01.Domain.Repositories.IWeatherRepository, ApiSample01.Infrastructure.Repositories.WeatherRepository>();

// Registra o serviço de aplicação
builder.Services.AddScoped<ApiSample01.Application.Interfaces.IWeatherForecastApplicationService, ApiSample01.Application.Services.WeatherForecastApplicationService>();

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeia controllers
app.MapControllers();

await app.RunAsync();
