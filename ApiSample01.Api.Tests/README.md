# 🧪 Presentation Layer Tests (API)

## 📋 Visão Geral

Testes unitários para a camada **Presentation (API)** seguindo os princípios da Clean Architecture.

## 🏗️ Estrutura de Testes

```
ApiSample01.Api.Tests/
├── Controllers/
│   └── WeatherForecastApiControllerTests.cs ✅
└── Models/
    └── WeatherForecastRequestTests.cs ✅
```

## ✅ Testes Implementados

### **WeatherForecastApiControllerTests** (13 testes)

#### **Cenários Testados:**

**Constructor:**
- ✅ **Constructor_ShouldCreateInstance_WhenValidService**

**Get Method:**
- ✅ **Get_ShouldReturnOk_WhenServiceReturnsSuccess**
- ✅ **Get_ShouldReturnBadRequest_WhenServiceReturnsError**
- ✅ **Get_ShouldReturnInternalServerError_WhenServiceReturns500**
- ✅ **Get_ShouldUseDefaultValues_WhenRequestHasDefaults**
- ✅ **Get_ShouldPassCorrectParameters_WhenCalled** (Theory: múltiplos cenários)

**Controller Attributes:**
- ✅ **Controller_ShouldHaveApiControllerAttribute**
- ✅ **Controller_ShouldHaveCorrectRoute** ("weather/api/v1")
- ✅ **Get_ShouldHaveHttpGetAttribute** ("forecast")

### **WeatherForecastRequestTests** (18 testes)

#### **Cenários Testados:**

**Constructor:**
- ✅ **Constructor_ShouldSetDefaultValues_WhenCreated** (Days=2, Start=1, Limit=100)

**Properties:**
- ✅ **Properties_ShouldBeSettable_WhenAssigned**
- ✅ **Properties_ShouldAcceptValidValues** (Theory: múltiplos cenários)

**Validation Attributes:**
- ✅ **Days_ShouldHaveDaysValidationAttribute**
- ✅ **Start_ShouldHaveStartValidationAttribute**
- ✅ **Limit_ShouldHaveLimitValidationAttribute**

**Model Validation:**
- ✅ **Model_ShouldBeValid_WithDefaultValues**
- ✅ **Model_ShouldBeValid_WithValidValues** (Theory: múltiplos cenários)
- ✅ **Model_ShouldThrowException_WithInvalidDays** (Theory: -1, 101)
- ✅ **Model_ShouldThrowException_WithInvalidStart** (Theory: 0, -1)
- ✅ **Model_ShouldThrowException_WithInvalidLimit** (Theory: 0, 101)

**Type Tests:**
- ✅ **Properties_ShouldHaveCorrectTypes**
- ✅ **Properties_ShouldHavePublicGettersAndSetters**

## 🎯 Cobertura de Testes

### **WeatherForecastApiController**: 100% ✅ (13 testes)
- ✅ Injeção de dependência (IWeatherForecastApplicationService)
- ✅ Mapeamento de Result pattern para HTTP responses
- ✅ Status codes corretos (200, 400, 500)
- ✅ Routing e attributes ([ApiController], [Route], [HttpGet])
- ✅ Mocking com Moq framework
- ✅ Valores padrão do request model

### **WeatherForecastRequest**: 100% ✅ (18 testes)
- ✅ Propriedades com valores padrão
- ✅ Validation attributes customizados
- ✅ Model validation com DataAnnotations
- ✅ Exception handling nos validators
- ✅ Tipos e acessibilidade das propriedades

## 📊 Resultados dos Testes

```
Passed!  - Failed: 0, Passed: 31, Skipped: 0, Total: 31
```

## 🎯 Características dos Testes

- **✅ Unit Testing** - Testes isolados com mocks
- **✅ HTTP Status Codes** - Validação de responses corretos
- **✅ Model Validation** - DataAnnotations e custom validators
- **✅ Attribute Testing** - Routing e controller attributes
- **✅ Exception Handling** - Validators que lançam exceções
- **✅ Result Pattern** - Mapeamento Success/Error → HTTP
- **✅ Dependency Injection** - Mocking de services

## 🏛️ Arquitetura Presentation

### **Responsabilidades:**
- ✅ **HTTP Handling** - Recebe requests, retorna responses
- ✅ **Model Binding** - Query parameters → Request models
- ✅ **Validation** - DataAnnotations e custom validators
- ✅ **Status Mapping** - Result pattern → HTTP status codes
- ✅ **Serialization** - JSON input/output
- ✅ **Routing** - URL mapping para actions

### **Padrões Aplicados:**
- ✅ **Controller Pattern** - MVC controllers
- ✅ **Model Validation** - DataAnnotations
- ✅ **Dependency Injection** - Constructor injection
- ✅ **Result Pattern** - Success/Error handling
- ✅ **Clean Architecture** - Presentation → Application

## 🧪 Técnicas de Teste

### **Controller Testing:**
```csharp
Mock<IWeatherForecastApplicationService> _mockService;
var result = Result<WeatherForecastApiResponseDto, ApiErrorResponse>.Ok(response);
_mockService.Setup(s => s.GetWeatherForecastApi(5, 1, 10)).ReturnsAsync(result);
```

### **Model Validation Testing:**
```csharp
var context = new ValidationContext(request);
var results = new List<ValidationResult>();
var isValid = Validator.TryValidateObject(request, context, results, true);
```

### **Attribute Testing:**
```csharp
var attributes = typeof(WeatherForecastApiController)
    .GetCustomAttributes(typeof(ApiControllerAttribute), false);
```

## 🚀 Próximos Passos

### **Expansões Possíveis:**
- **Integration Tests** - TestServer com WebApplicationFactory
- **End-to-End Tests** - Testes completos da API
- **Performance Tests** - Load testing
- **Security Tests** - Authentication/Authorization

### **Outras Funcionalidades:**
- **Middleware Tests** - Custom middleware
- **Filter Tests** - Action filters
- **Exception Handling** - Global exception handling

## 📝 Comandos Úteis

```bash
# Executar testes da API
dotnet test ApiSample01.Api.Tests/

# Executar com cobertura
dotnet test ApiSample01.Api.Tests/ --collect:"XPlat Code Coverage"

# Executar teste específico
dotnet test --filter "WeatherForecastApiControllerTests"
```

## 🎉 Status

**Presentation Layer: 100% Testado!** ✅

- **1 Controller** completamente coberto
- **1 Model** completamente coberto
- **31 Testes** passando sem falhas
- **100% Cobertura** de funcionalidades
- **HTTP Responses** e **Model Validation** validados