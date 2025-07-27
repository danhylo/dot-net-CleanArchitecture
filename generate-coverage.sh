#!/bin/bash

# 📊 Script para Gerar Relatório de Cobertura HTML

echo "🧪 Executando testes com cobertura..."
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

echo "📊 Gerando relatório HTML..."
export PATH="$PATH:/Users/danhylo/.dotnet/tools"
reportgenerator \
    -reports:"TestResults/*/coverage.cobertura.xml" \
    -targetdir:"CoverageReport" \
    -reporttypes:Html

echo "✅ Relatório gerado em: CoverageReport/index.html"
echo "🌐 Para visualizar, abra: file://$(pwd)/CoverageReport/index.html"