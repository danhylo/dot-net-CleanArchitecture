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

### **📈 Resumo por Camada**
| Camada | Testes | Line Coverage | Status |
|--------|--------|---------------|--------|
| **Domain** | 166 | 100.0% | ✅ Excelente |
| **Application** | 24 | 100.0% | ✅ Excelente |
| **Infrastructure** | 12 | 100.0% | ✅ Excelente |
| **API** | 35 | 100.0% | ✅ Excelente |
| **Total** | **237** | **100.0%** | ✅ **Perfeito** |

### **Domain Layer** - ✅ 100.0% (166 testes)
- ✅ **ET002FieldSizeError**: 2 testes
- ✅ **BaseException**: 2 testes
- ✅ **WeatherForecastBusinessRules**: 4 testes
- ✅ **WeatherForecastDomainService**: 2 testes
- ✅ **ValueObjects**: 156 testes
  - DaysRange: 52 testes
  - LimitValue: 52 testes
  - StartValue: 52 testes

### **Application Layer** - ✅ 100.0% (24 testes)
- ✅ **WeatherForecastApplicationService**: 8 testes
- ✅ **WeatherForecastApiRequestDto**: 12 testes
- ✅ **ValidationExtensions**: 3 testes
- ✅ **PaginationExtensions**: 1 teste

### **Infrastructure Layer** - ✅ 100.0% (12 testes)
- ✅ **WeatherRepository**: 12 testes

### **Presentation Layer (API)** - ✅ 100.0% (35 testes)
- ✅ **WeatherForecastApiController**: 11 testes
- ✅ **WeatherForecastRequest**: 22 testes
- ✅ **ModelValidationFilter**: 4 testes
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

### **Métricas Atuais vs Metas:**
| Métrica | Atual | Meta | Status |
|---------|-------|------|--------|
| **Line Coverage** | 100.0% | ≥ 90% | ✅ Perfeito |
| **Branch Coverage** | N/A | ≥ 85% | ⏳ Pendente |
| **Method Coverage** | N/A | ≥ 95% | ⏳ Pendente |

### **🎆 Cobertura Perfeita Atingida!**
1. ✅ **Line Coverage**: 100.0% - Todas as linhas testáveis cobertas
2. ✅ **Todas as Camadas**: 100% de cobertura em todos os projetos
3. 🎯 **Próximos Passos**: Implementar Branch e Method Coverage
4. 🏆 **Resultado**: Cobertura exemplar seguindo Clean Architecture

## 🚀 Integração CI/CD

### **GitHub Actions**
```yaml
- name: Test with Coverage
  run: dotnet test --collect:"XPlat Code Coverage"
  
- name: Generate Coverage Report
  run: reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html
```

## 🔍 Análise Detalhada por Projeto

### **🏆 Domain Tests** - [README](ApiSample01.Domain.Tests/README.md)
- **Cobertura**: 100.0% ✅
- **Testes**: 166 (todos passando)
- **Status**: Excelente - Todas as regras de negócio cobertas

### **💼 Application Tests** - [README](ApiSample01.Application.Tests/README.md)
- **Cobertura**: 100.0% ✅
- **Testes**: 24 (todos passando)
- **Status**: Excelente - Totalmente coberto

### **💾 Infrastructure Tests** - [README](ApiSample01.Infrastructure.Tests/README.md)
- **Cobertura**: 100.0% ✅
- **Testes**: 12 (todos passando)
- **Status**: Excelente - Totalmente coberto

### **🌐 API Tests** - [README](ApiSample01.Api.Tests/README.md)
- **Cobertura**: 100.0% ✅
- **Testes**: 35 (todos passando)
- **Status**: Excelente - Totalmente coberto (Program.cs excluído)

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