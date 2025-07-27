# Mapa de Interação - Clean Architecture

## 🏗️ Estrutura das Camadas

```
┌─────────────────────────────────────────────────────────┐
│ PRESENTATION LAYER (Controllers/API)                    │
│ • WeatherForecastApiController.cs                       │
│ • WeatherForecastRequest.cs (Model)                     │
│ • Recebe HTTP requests                                  │
│ • Valida entrada                                        │
│ • Retorna DTOs/JSON                                     │
└─────────────────────┬───────────────────────────────────┘
                      ▼
┌─────────────────────────────────────────────────────────┐
│ APPLICATION LAYER (Services/Use Cases)                  │
│ • WeatherForecastApplicationService.cs                  │
│ • IWeatherForecastApplicationService.cs                 │
│ • WeatherForecastApiResponseDto.cs                      │
│ • Orquestra fluxo de negócio                            │
│ • Converte Domain → DTO                                 │
│ • Gerencia transações                                   │
└─────────────────────┬───────────────────────────────────┘
                      ▼
┌─────────────────────────────────────────────────────────┐
│ DOMAIN LAYER (Entities/Business Rules)                  │
│ • WeatherForecast.cs (Entity)                           │
│ • IWeatherRepository.cs (Interface)                     │
│ • WeatherForecastDomainService.cs                       │
│ • WeatherForecastBusinessRules.cs                       │
│ • ValueObjects (DaysRange, LimitValue, StartValue)      │
│ • Regras de negócio puras                               │
│ • Validações de domínio                                 │
└─────────────────────▲───────────────────────────────────┘
                      │
┌─────────────────────────────────────────────────────────┐
│ INFRASTRUCTURE LAYER (Data/External)                    │
│ • WeatherRepository.cs                                  │
│ • Implementa interfaces do Domain                       │
│ • Geração de dados simulados                            │
└─────────────────────────────────────────────────────────┘
```

## 🔄 Fluxo de Interação Completo

### 1. Request Flow (Controller → Repository)
```
HTTP GET /weather/api/v1/forecast
    ↓
WeatherForecastApiController
    ↓ [chama]
WeatherForecastApplicationService (Application)
    ↓ [usa]
WeatherForecast (Domain Entity)
    ↓ [através de]
IWeatherRepository (Domain Interface)
    ↓ [implementado por]
WeatherRepository (Infrastructure)
    ↓ [gera dados via]
WeatherForecastDomainService
```

### 2. Response Flow (Repository → Controller)
```
WeatherForecastDomainService
    ↓ [gera dados simulados]
WeatherRepository
    ↓ [retorna]
WeatherForecast Entities (Domain)
    ↓ [paginado e convertido para]
WeatherForecastApiResponseDto (Application)
    ↓ [retornado para]
WeatherForecastApiController
    ↓ [serializado para]
JSON Response
```

## 📋 Responsabilidades por Camada

### **WeatherForecastApiController (Presentation)**
- Recebe requisições HTTP GET /weather/api/v1/forecast
- Valida WeatherForecastRequest (Days, Start, Limit)
- Chama WeatherForecastApplicationService
- Retorna HTTP Response com WeatherForecastApiResponseDto

### **WeatherForecastApplicationService (Application)**
- Orquestra casos de uso de previsão do tempo
- Aplica paginação (Start, Limit)
- Converte WeatherForecast Entity → DTO
- Gerencia transações e tratamento de erros
- Coordena Domain + Infrastructure

### **WeatherForecast Entity (Domain)**
- Contém dados da previsão (Date, TemperatureC, TemperatureF, Summary)
- WeatherForecastDomainService gera dados simulados
- WeatherForecastBusinessRules aplica regras de negócio
- ValueObjects validam entrada (DaysRange, LimitValue, StartValue)
- Independente de infraestrutura

### **WeatherRepository (Infrastructure)**
- Implementa IWeatherRepository do Domain
- Gera dados simulados via WeatherForecastDomainService
- Simula acesso a dados externos
- Retorna coleção de WeatherForecast

## 🎯 Regras de Dependência

```
Presentation → Application → Domain ← Infrastructure
```

- **Presentation**: Conhece Application e Domain
- **Application**: Conhece apenas Domain
- **Domain**: Independente (núcleo)
- **Infrastructure**: Conhece apenas Domain

## 📝 Exemplo Prático

```csharp
// 1. Controller recebe request
[HttpGet("forecast")]
public async Task<IActionResult> Get([FromQuery] WeatherForecastRequest request)
    ↓
// 2. Chama Application Service
var result = await _weatherForecastApplicationService.GetWeatherForecastApi(
    request.Days, request.Start, request.Limit);
    ↓
// 3. Service usa Domain Repository
var forecastsData = await _weatherRepository.GetForecastsAsync(request.Days);
    ↓
// 4. Repository gera dados via Domain Service
var forecasts = WeatherForecastDomainService.GenerateForecasts(days);
    ↓
// 5. Retorna WeatherForecast[] → WeatherForecastApiResponseDto → JSON
```

## 🔧 Injeção de Dependência

```csharp
// Domain Interface
IWeatherRepository → WeatherRepository (Infrastructure)

// Application Interface  
IWeatherForecastApplicationService → WeatherForecastApplicationService (Application)

// Controller usa
WeatherForecastApplicationService através de IWeatherForecastApplicationService
```

Esta arquitetura garante baixo acoplamento, alta coesão e facilita testes unitários.