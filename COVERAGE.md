# 📊 Mapa de Cobertura de Testes

## 🎯 Como Gerar Relatório de Cobertura HTML

### **Método 1: Script Automatizado**
```bash
./generate-coverage.sh
```

### **Método 2: Comandos Manuais**

#### 1. Executar testes com cobertura:
```bash
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults
```

#### 2. Gerar relatório HTML:
```bash
reportgenerator \
    -reports:"TestResults/*/coverage.cobertura.xml" \
    -targetdir:"CoverageReport" \
    -reporttypes:Html
```

#### 3. Visualizar relatório:
```bash
open CoverageReport/index.html
```

## 📁 Estrutura de Arquivos Gerados

```
CoverageReport/
├── index.html                              # 📊 Relatório principal
├── ApiSample01.Api_WeatherForecastApiController.html
├── ApiSample01.Api_WeatherForecastRequest.html
├── ApiSample01.Api_ModelValidationFilter.html
├── ApiSample01.Application_WeatherForecastApplicationService.html
├── ApiSample01.Application_WeatherForecastApiRequestDto.html
├── ApiSample01.Application_ValidationExtensions.html
├── ApiSample01.Domain_ET002FieldSizeError.html
├── ApiSample01.Domain_BaseException.html
├── ApiSample01.Domain_WeatherForecastBusinessRules.html
├── ApiSample01.Domain_WeatherForecastDomainService.html
├── ApiSample01.Domain_DaysRange.html
├── ApiSample01.Domain_LimitValue.html
├── ApiSample01.Domain_StartValue.html
├── ApiSample01.Infrastructure_WeatherRepository.html
└── [assets CSS/JS/SVG]                     # 🎨 Recursos visuais
```

## 📈 Tipos de Relatório Disponíveis

### **HTML** (Recomendado)
- Interface interativa
- Navegação por classes
- Destaque de código coberto/não coberto
- Métricas detalhadas

### **Outros Formatos**
```bash
# XML Cobertura
-reporttypes:Cobertura

# Badge SVG
-reporttypes:Badges

# Texto simples
-reporttypes:TextSummary

# Múltiplos formatos
-reporttypes:Html;Badges;TextSummary
```

## 🎯 Métricas de Cobertura

### **Line Coverage** (Cobertura de Linhas)
- Percentual de linhas executadas pelos testes

### **Branch Coverage** (Cobertura de Branches)
- Percentual de caminhos condicionais testados

### **Method Coverage** (Cobertura de Métodos)
- Percentual de métodos executados

## 📊 Status Atual da Cobertura

### **Domain Layer**
- ✅ **ET002FieldSizeError**: 100% (2/2 testes)
- ✅ **BaseException**: 100% (2/2 testes)
- ✅ **WeatherForecastBusinessRules**: 100% (4/4 testes)
- ✅ **WeatherForecastDomainService**: 100% (2/2 testes)
- ✅ **ValueObjects**: 100% (12/12 testes)
  - DaysRange: 4/4 testes
  - LimitValue: 4/4 testes
  - StartValue: 4/4 testes
  - ValueObject: 0/0 testes (classe abstrata)

### **Application Layer**
- ✅ **WeatherForecastApplicationService**: 100% (8/8 testes)
- ✅ **WeatherForecastApiRequestDto**: 100% (12/12 testes)
- ✅ **ValidationExtensions**: 100% (3/3 testes)

### **Infrastructure Layer**
- ✅ **WeatherRepository**: 100% (1/1 teste)

### **Presentation Layer (API)**
- ✅ **WeatherForecastApiController**: 100% (11/11 testes)
- ✅ **WeatherForecastRequest**: 100% (22/22 testes)
- ✅ **ModelValidationFilter**: 100% (4/4 testes)
- 🚫 **Program.cs**: Excluído da cobertura (configuração)

## 🔧 Configurações Avançadas

### **Filtros de Cobertura**
```bash
# Excluir arquivos de teste
-reporttypes:Html -classfilters:-*Tests*

# Incluir apenas Domain
-reporttypes:Html -assemblyfilters:+ApiSample01.Domain

# Excluir Program.cs (já configurado)
-filefilters:"-**/Program.cs"
```

### **Configuração via arquivo**
Criar `coverlet.runsettings`:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<RunSettings>
  <DataCollectionRunSettings>
    <DataCollectors>
      <DataCollector friendlyName="XPlat code coverage">
        <Configuration>
          <Format>cobertura</Format>
          <Exclude>[*Tests*]*</Exclude>
        </Configuration>
      </DataCollector>
    </DataCollectors>
  </DataCollectionRunSettings>
</RunSettings>
```

## 🎯 Metas de Cobertura

### **Objetivos por Camada:**
- 🎯 **Domain**: 95%+ (regras críticas de negócio)
- 🎯 **Application**: 90%+ (orquestração e DTOs)
- 🎯 **Infrastructure**: 80%+ (implementações)
- 🎯 **Presentation**: 85%+ (controllers e validações)

### **Métricas Mínimas:**
- ✅ **Line Coverage**: ≥ 90%
- ✅ **Branch Coverage**: ≥ 85%
- ✅ **Method Coverage**: ≥ 95%

## 🚀 Integração CI/CD

### **GitHub Actions**
```yaml
- name: Test with Coverage
  run: dotnet test --collect:"XPlat Code Coverage"
  
- name: Generate Coverage Report
  run: reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html
```

## 📝 Comandos Úteis

```bash
# Cobertura apenas do Domain
dotnet test ApiSample01.Domain.Tests/ --collect:"XPlat Code Coverage"

# Cobertura com threshold
dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Threshold=90

# Limpar resultados anteriores
rm -rf TestResults/ CoverageReport/
```

## 🌐 Visualização

**Abrir relatório:**
- **macOS**: `open CoverageReport/index.html`
- **Windows**: `start CoverageReport/index.html`
- **Linux**: `xdg-open CoverageReport/index.html`

**URL local:**
```
file:///[caminho-completo]/CoverageReport/index.html
```