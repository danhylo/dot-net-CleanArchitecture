# 🐳 Docker - WeatherForecast API

## 📋 Pré-requisitos

- Docker instalado
- Docker Compose instalado

## 🔨 Build da Imagem

```bash
# Limpar cache de build (opcional)
docker builder prune -a

# Build da imagem
docker build -t weatherforecast-api .
```

## 🚀 Executar com Docker Compose

```bash
# Executar em background
docker-compose up -d

# Executar com logs visíveis
docker-compose up

# Parar os containers
docker-compose down
```

## 🌐 Acessar a Aplicação

### 📊 Swagger UI (Development)
- **URL**: http://localhost:8090/swagger
- **Disponível apenas** quando `ASPNETCORE_ENVIRONMENT=Development`

### 🔗 API Endpoints
- **Base URL**: http://localhost:8090
- **Weather Forecast**: http://localhost:8090/weather/api/v1/forecast

### 📝 Exemplo de Requisição

```bash
# Requisição básica
curl http://localhost:8090/weather/api/v1/forecast

# Com parâmetros
curl "http://localhost:8090/weather/api/v1/forecast?days=5&start=1&limit=10"
```

## ⚙️ Configurações

### 🔧 Variáveis de Ambiente

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Development  # ou Production
  - ASPNETCORE_URLS=http://0.0.0.0:8080
  - DOTNET_RUNNING_IN_CONTAINER=true
```

### 🔌 Portas

- **Container**: 8080 (interna)
- **Host**: 8090 (externa)

## 🛠️ Comandos Úteis

```bash
# Ver logs do container
docker-compose logs -f

# Rebuild forçado
docker-compose up --build

# Remover containers e volumes
docker-compose down -v

# Status dos containers
docker-compose ps
```

## 🎯 Ambientes

| Ambiente | Swagger | API |
|----------|---------|-----|
| **Development** | ✅ Disponível | ✅ Disponível |
| **Production** | ❌ Desabilitado | ✅ Disponível |

---

> 💡 **Dica**: Para produção, altere `ASPNETCORE_ENVIRONMENT=Production` no docker-compose.yml