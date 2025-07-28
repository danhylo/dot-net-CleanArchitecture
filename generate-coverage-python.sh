#!/bin/bash

# 📊 Script usando Python Parser (100% Gratuito)

echo "🧪 Executando testes com cobertura..."
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

echo "🐍 Gerando relatório com Python..."
python3 coverage-parser.py

echo "✅ Relatório gerado em: CoverageReport/index.html"
echo "🌐 Para visualizar: open CoverageReport/index.html"