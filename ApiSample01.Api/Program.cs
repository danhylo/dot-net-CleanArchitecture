var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços para Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiSample01", Version = "v1" });
});

// Adiciona suporte a controllers
builder.Services.AddControllers();

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
