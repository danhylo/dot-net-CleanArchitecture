# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ApiSample01.Api/ApiSample01.Api.csproj ApiSample01.Api/
COPY ApiSample01.Application/ApiSample01.Application.csproj ApiSample01.Application/
COPY ApiSample01.Domain/ApiSample01.Domain.csproj ApiSample01.Domain/
COPY ApiSample01.Infrastructure/ApiSample01.Infrastructure.csproj ApiSample01.Infrastructure/

# Restore dependencies
RUN dotnet restore ApiSample01.Api/ApiSample01.Api.csproj

# Copy source code (excluding test projects)
COPY ApiSample01.Api/ ApiSample01.Api/
COPY ApiSample01.Application/ ApiSample01.Application/
COPY ApiSample01.Domain/ ApiSample01.Domain/
COPY ApiSample01.Infrastructure/ ApiSample01.Infrastructure/

# Build and publish
RUN dotnet publish ApiSample01.Api/ApiSample01.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Create non-root user
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Run the application
ENTRYPOINT ["dotnet", "ApiSample01.Api.dll"]