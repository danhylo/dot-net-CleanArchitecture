# 🧪 Application Layer Tests

## 📋 Visão Geral

Testes unitários para a camada **Application** seguindo os princípios da Clean Architecture.

## 🏗️ Estrutura de Testes

```
ApiSample01.Application.Tests/
├── Services/
│   └── WeatherForecastApplicationServiceTests.cs ✅
└── Dto/
    └── WeatherForecastApiRequestDtoTests.cs ✅
```

## ✅ Testes Implementados

### **WeatherForecastApplicationServiceTests** (13 testes)

#### **Cenários Testados:**

**Constructor:**
- ✅ **Constructor_ShouldCreateInstance_WhenValidRepository**

**GetWeatherForecast:**
- ✅ **GetWeatherForecast_ShouldReturnForecasts_WhenValidParameters**
- ✅ **GetWeatherForecast_ShouldThrowException_WhenInvalidDays**
- ✅ **GetWeatherForecast_ShouldThrowException_WhenInvalidStart**
- ✅ **GetWeatherForecast_ShouldThrowException_WhenInvalidLimit**

**GetWeatherForecastApi:**
- ✅ **GetWeatherForecastApi_ShouldReturnSuccessResult_WhenValidParameters**
- ✅ **GetWeatherForecastApi_ShouldReturnFailResult_WhenInvalidDays**
- ✅ **GetWeatherForecastApi_ShouldReturnFailResult_WhenRepositoryThrows**
- ✅ **GetWeatherForecastApi_ShouldSetCorrectPageInfo_WhenCalled**

### **WeatherForecastApiRequestDtoTests** (12 testes)

#### **Cenários Testados:**

**Constructor:**
- ✅ **Constructor_ShouldCreateDto_WhenValidParameters**
- ✅ **Constructor_ShouldCreateValueObjects_WhenCalled**
- ✅ **Constructor_ShouldWork_WithValidRanges** (Theory: múltiplos cenários)

**Validation:**
- ✅ **Constructor_ShouldThrowException_WhenInvalidDays**
- ✅ **Constructor_ShouldThrowException_WhenInvalidStart**
- ✅ **Constructor_ShouldThrowException_WhenInvalidLimit**

**Properties:**
- ✅ **Properties_ShouldBeReadOnly_WhenAccessed**
- ✅ **Properties_ShouldHaveCorrectTypes**

**Edge Cases:**
- ✅ **Constructor_ShouldWork_WithMinimumValues**
- ✅ **Constructor_ShouldWork_WithMaximumValues**

## 🎯 Cobertura de Testes

### **WeatherForecastApplicationService**: 100% ✅ (13 testes)
- ✅ Implementação da interface IWeatherForecastApplicationService
- ✅ Injeção de dependência (IWeatherRepository)
- ✅ Orquestração de Domain e Infrastructure
- ✅ Paginação com extensões
- ✅ Tratamento de exceções (ET002FieldSizeError)
- ✅ Result pattern para API responses
- ✅ Mocking com Moq framework

### **WeatherForecastApiRequestDto**: 100% ✅ (12 testes)
- ✅ Construção com ValueObjects
- ✅ Validação através de ValueObjects
- ✅ Propriedades read-only
- ✅ Tipos corretos (DaysRange, StartValue, LimitValue)
- ✅ Edge cases (valores mínimos e máximos)

## 📊 Resultados dos Testes

```
Passed!  - Failed: 0, Passed: 25, Skipped: 0, Total: 25
```

## 🎯 Características dos Testes

- **✅ Mocking** - Usa Moq para IWeatherRepository
- **✅ Dependency Injection** - Testa injeção de dependências
- **✅ Result Pattern** - Valida Success/Error responses
- ✅ **Exception Handling** - Testa cenários de erro
- **✅ DTO Validation** - ValueObjects com validação
- **✅ Pagination** - Testa paginação de resultados
- **✅ Integration** - Coordena Domain e Infrastructure

## 🏛️ Arquitetura Application

### **Responsabilidades:**
- ✅ **Orquestração** - Coordena Domain e Infrastructure
- ✅ **DTOs** - Transferência de dados entre camadas
- ✅ **Use Cases** - Implementa casos de uso da aplicação
- ✅ **Result Pattern** - Encapsula success/error responses
- ✅ **Paginação** - Implementa paginação de dados
- ✅ **Exception Handling** - Trata exceções do Domain

### **Padrões Aplicados:**
- ✅ **Service Layer** - Application Services
- ✅ **DTO Pattern** - Data Transfer Objects
- ✅ **Result Pattern** - Success/Error handling
- ✅ **Dependency Injection** - Inversão de controle
- ✅ **Clean Architecture** - Orquestra Domain e Infrastructure

## 🧪 Técnicas de Teste

### **Mocking:**
```csharp
Mock<IWeatherRepository> _mockRepository;
_mockRepository.Setup(r => r.GetForecastsAsync(5)).ReturnsAsync(forecasts);
```

### **Result Pattern Testing:**
```csharp
Assert.True(result.IsSuccess);
Assert.NotNull(result.Success);
Assert.Equal(200, result.Success.HttpCode);
```

### **Exception Testing:**
```csharp
await Assert.ThrowsAsync<ET002FieldSizeError>(() => 
    _service.GetWeatherForecast(-1, 1, 10));
```

## 🚀 Próximos Passos

### **Expansões Possíveis:**
- **Integration Tests** - Testes de integração completos
- **Performance Tests** - Testes de performance
- **Validation Tests** - Mais cenários de validação
- **Caching Tests** - Se implementar cache

### **Outras Camadas:**
- **Presentation Layer Tests** - Controllers e Models
- **End-to-End Tests** - Testes completos da API

## 📝 Comandos Úteis

```bash
# Executar testes da Application
dotnet test ApiSample01.Application.Tests/

# Executar com cobertura
dotnet test ApiSample01.Application.Tests/ --collect:"XPlat Code Coverage"

# Executar teste específico
dotnet test --filter "WeatherForecastApplicationServiceTests"
```

## 🎉 Status

**Application Layer: 100% Testado!** ✅

- **1 Service** completamente coberto
- **1 DTO** completamente coberto
- **25 Testes** passando sem falhas
- **100% Cobertura** de funcionalidades
- **Mocking** e **Result Pattern** validados