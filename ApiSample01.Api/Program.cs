using ApiSample01.Domain;
// Usings removidos, pois não são necessários para métodos de extensão do Swagger

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços para Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiSample01", Version = "v1" });
});

// Adiciona suporte a controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Removido endpoint e dados duplicados, agora o controller cuida do endpoint

// Mapeia controllers
app.MapControllers();


await app.RunAsync();


