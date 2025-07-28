#!/bin/bash

# 📊 Script Simples para Cobertura (Sem ReportGenerator)

echo "🧪 Executando testes com cobertura..."
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

echo "📊 Gerando relatório JSON..."
dotnet test --collect:"XPlat Code Coverage" --logger:"json;LogFileName=coverage.json" --results-directory ./TestResults

echo "📋 Resumo de Cobertura:"
echo "========================"

# Extrair informações básicas do XML
if [ -f TestResults/*/coverage.cobertura.xml ]; then
    COVERAGE_FILE=$(find TestResults -name "coverage.cobertura.xml" | head -1)
    
    # Usar xmllint se disponível (macOS: brew install libxml2)
    if command -v xmllint &> /dev/null; then
        LINE_RATE=$(xmllint --xpath "string(/coverage/@line-rate)" "$COVERAGE_FILE")
        BRANCH_RATE=$(xmllint --xpath "string(/coverage/@branch-rate)" "$COVERAGE_FILE")
        
        LINE_PERCENT=$(echo "$LINE_RATE * 100" | bc -l | xargs printf "%.1f")
        BRANCH_PERCENT=$(echo "$BRANCH_RATE * 100" | bc -l | xargs printf "%.1f")
        
        echo "📈 Cobertura de Linhas: ${LINE_PERCENT}%"
        echo "🌿 Cobertura de Branches: ${BRANCH_PERCENT}%"
    else
        echo "📄 Arquivo de cobertura: $COVERAGE_FILE"
        echo "💡 Instale xmllint para ver percentuais: brew install libxml2"
    fi
else
    echo "❌ Arquivo de cobertura não encontrado"
fi

echo ""
echo "✅ Para relatório detalhado, use: ./generate-coverage.sh"
echo "📁 Arquivos em: TestResults/"