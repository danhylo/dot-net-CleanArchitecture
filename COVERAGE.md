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
├── ApiSample01.Domain_ET002FieldSizeError.html  # 🔍 Detalhes por classe
├── ApiSample01.Domain_WeatherForecast.html
├── ApiSample01.Domain_WeatherForecastBusinessRules.html
├── ApiSample01.Domain_WeatherForecastDomainService.html
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
- ✅ **ET002FieldSizeError**: 100% (7/7 testes)
- ⏳ **WeatherForecastBusinessRules**: Pendente
- ⏳ **WeatherForecastDomainService**: Pendente
- ⏳ **WeatherForecast**: Pendente
- ⏳ **ValueObjects**: Pendente

## 🔧 Configurações Avançadas

### **Filtros de Cobertura**
```bash
# Excluir arquivos de teste
-reporttypes:Html -classfilters:-*Tests*

# Incluir apenas Domain
-reporttypes:Html -assemblyfilters:+ApiSample01.Domain
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