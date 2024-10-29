using ApiConversion.Interfaces;
using ApiConversion.Models;
using ApiConversion.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações do appsettings.json
builder.Services.Configure<ApiConversionSettings>(
    builder.Configuration.GetSection("ExchangeRateAPI"));

// Configurar o HttpClient com injeção de dependência
builder.Services.AddHttpClient<IConversionRate, ConversionRateService>();

// Adicionar serviços de controle
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Exchange Rate API",
        Version = "v1",
        Description = "API para obter taxas de câmbio entre moedas."
    });
});

var app = builder.Build();

// Middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchange Rate API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
