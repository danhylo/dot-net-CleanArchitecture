#!/bin/bash

# 📊 Script para Gerar Relatório de Cobertura HTML
# Suporta ReportGenerator e Coverlet com opção de escolha

# Adicionar .NET tools ao PATH
export PATH="$PATH:/Users/danhylo/.dotnet/tools"

# Função para mostrar ajuda
show_help() {
    echo "📊 Gerador de Relatório de Cobertura"
    echo ""
    echo "Uso: $0 [OPÇÃO]"
    echo ""
    echo "Opções:"
    echo "  -r, --reportgenerator    Usar ReportGenerator (padrão)"
    echo "  -c, --coverlet          Usar Coverlet"
    echo "  -h, --help              Mostrar esta ajuda"
    echo ""
    echo "Exemplos:"
    echo "  $0                      # Usar ReportGenerator (ou fallback automático)"
    echo "  $0 -r                   # Forçar ReportGenerator"
    echo "  $0 -c                   # Forçar Coverlet"
}

# Processar argumentos
TOOL=""
while [[ $# -gt 0 ]]; do
    case $1 in
        -r|--reportgenerator)
            TOOL="reportgenerator"
            shift
            ;;
        -c|--coverlet)
            TOOL="coverlet"
            shift
            ;;
        -h|--help)
            show_help
            exit 0
            ;;
        *)
            echo "❌ Opção desconhecida: $1"
            show_help
            exit 1
            ;;
    esac
done

# Executar testes apenas se não for Coverlet (que executa os próprios testes)
if [ "$TOOL" != "coverlet" ]; then
    echo "🧪 Executando testes com cobertura..."
    dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults
fi

echo "📊 Gerando relatório HTML..."

# Função para usar ReportGenerator
use_reportgenerator() {
    if command -v reportgenerator &> /dev/null; then
        echo "📈 Usando ReportGenerator..."
        reportgenerator \
            -reports:"TestResults/*/coverage.cobertura.xml" \
            -targetdir:"CoverageReport" \
            -reporttypes:Html \
            -filefilters:"-**/Program.cs"
        return 0
    else
        echo "❌ ReportGenerator não encontrado"
        echo "💡 Instale: dotnet tool install --global dotnet-reportgenerator-globaltool"
        return 1
    fi
}

# Função para usar Coverlet
use_coverlet() {
    if command -v coverlet &> /dev/null; then
        echo "📈 Usando Coverlet..."
        
        # Limpar resultados anteriores
        rm -rf TestResults CoverageReport
        mkdir -p CoverageReport
        
        # Executar testes com Coverlet diretamente
        echo "🧪 Executando testes com Coverlet..."
        
        # Encontrar projetos de teste
        TEST_PROJECTS=$(find . -name "*.Tests.csproj" -type f)
        
        if [ -z "$TEST_PROJECTS" ]; then
            echo "❌ Nenhum projeto de teste encontrado"
            return 1
        fi
        
        # Executar coverlet para cada projeto de teste
        TOTAL_LINE_RATE=0
        TOTAL_BRANCH_RATE=0
        PROJECT_COUNT=0
        
        for TEST_PROJECT in $TEST_PROJECTS; do
            PROJECT_NAME=$(basename "$TEST_PROJECT" .csproj)
            echo "📊 Processando: $PROJECT_NAME"
            
            # Usar formato cobertura (XML) que é suportado
            coverlet "$(dirname "$TEST_PROJECT")/bin/Debug/net9.0/$PROJECT_NAME.dll" \
                --target "dotnet" \
                --targetargs "test $TEST_PROJECT --no-build" \
                --format cobertura \
                --output "./CoverageReport/$PROJECT_NAME.xml" \
                --verbosity minimal
                
            # Extrair métricas se xmllint disponível
            if [ -f "./CoverageReport/$PROJECT_NAME.xml" ] && command -v xmllint &> /dev/null && command -v bc &> /dev/null; then
                LINE_RATE=$(xmllint --xpath "string(/coverage/@line-rate)" "./CoverageReport/$PROJECT_NAME.xml" 2>/dev/null || echo "0")
                BRANCH_RATE=$(xmllint --xpath "string(/coverage/@branch-rate)" "./CoverageReport/$PROJECT_NAME.xml" 2>/dev/null || echo "0")
                
                if [ "$LINE_RATE" != "" ] && [ "$LINE_RATE" != "0" ]; then
                    TOTAL_LINE_RATE=$(echo "$TOTAL_LINE_RATE + $LINE_RATE" | bc -l)
                    TOTAL_BRANCH_RATE=$(echo "$TOTAL_BRANCH_RATE + $BRANCH_RATE" | bc -l)
                    PROJECT_COUNT=$((PROJECT_COUNT + 1))
                fi
            fi
        done
        
        # Calcular médias se temos dados
        if [ $PROJECT_COUNT -gt 0 ] && command -v bc &> /dev/null; then
            AVG_LINE_RATE=$(echo "scale=3; $TOTAL_LINE_RATE / $PROJECT_COUNT" | bc -l)
            AVG_BRANCH_RATE=$(echo "scale=3; $TOTAL_BRANCH_RATE / $PROJECT_COUNT" | bc -l)
            
            LINE_PERCENT=$(echo "$AVG_LINE_RATE * 100" | bc -l | xargs printf "%.1f")
            BRANCH_PERCENT=$(echo "$AVG_BRANCH_RATE * 100" | bc -l | xargs printf "%.1f")
        else
            LINE_PERCENT="N/A"
            BRANCH_PERCENT="N/A"
        fi
        
        # Gerar índice HTML simples
        cat > CoverageReport/index.html << EOF
<!DOCTYPE html>
<html>
<head>
    <title>📊 Relatório de Cobertura - Coverlet</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .header { background: #f0f0f0; padding: 20px; border-radius: 8px; }
        .summary { background: #e8f4fd; padding: 15px; border-radius: 5px; margin: 20px 0; }
        .project { margin: 10px 0; padding: 10px; background: #f9f9f9; border-radius: 3px; }
        .metric { display: inline-block; margin: 10px; padding: 10px; background: #d4edda; border-radius: 5px; }
    </style>
</head>
<body>
    <div class="header">
        <h1>📊 Relatório de Cobertura</h1>
        <p>Gerado com Coverlet em: $(date)</p>
    </div>
    
    <div class="summary">
        <h2>📈 Resumo Geral</h2>
        <div class="metric">
            <strong>Cobertura de Linhas:</strong> ${LINE_PERCENT}%
        </div>
        <div class="metric">
            <strong>Cobertura de Branches:</strong> ${BRANCH_PERCENT}%
        </div>
    </div>
    
    <h2>📋 Projetos Analisados</h2>
EOF
        
        for TEST_PROJECT in $TEST_PROJECTS; do
            PROJECT_NAME=$(basename "$TEST_PROJECT" .csproj)
            if [ -f "CoverageReport/$PROJECT_NAME.xml" ]; then
                echo "    <div class='project'>" >> CoverageReport/index.html
                echo "        <h3>$PROJECT_NAME</h3>" >> CoverageReport/index.html
                echo "        <p>📄 Arquivo XML: <a href='$PROJECT_NAME.xml'>$PROJECT_NAME.xml</a></p>" >> CoverageReport/index.html
                echo "    </div>" >> CoverageReport/index.html
            fi
        done
        
        cat >> CoverageReport/index.html << EOF
    
    <div style="margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #666;">
        <p>💡 Para visualizar detalhes, use os arquivos XML com ferramentas como Visual Studio ou ReportGenerator</p>
        <p>🔧 Para HTML detalhado, use: <code>./generate-coverage.sh -r</code></p>
    </div>
EOF
        
        echo "</body></html>" >> CoverageReport/index.html
        
        # Mostrar resumo no terminal
        echo ""
        echo "📊 Resumo de Cobertura:"
        echo "📈 Linhas: ${LINE_PERCENT}%"
        echo "🌿 Branches: ${BRANCH_PERCENT}%"
        echo "📁 Arquivos XML gerados em: CoverageReport/"
        
        return 0
    else
        echo "❌ Coverlet não encontrado"
        echo "💡 Instale: dotnet tool install --global coverlet.console"
        return 1
    fi
}

# Executar ferramenta escolhida
case $TOOL in
    "reportgenerator")
        use_reportgenerator || exit 1
        ;;
    "coverlet")
        use_coverlet || exit 1
        ;;
    *)
        # Fallback automático (prioridade: ReportGenerator > Coverlet)
        if command -v reportgenerator &> /dev/null; then
            use_reportgenerator
        elif command -v coverlet &> /dev/null; then
            use_coverlet
        else
            echo "❌ Nem ReportGenerator nem Coverlet encontrados"
            echo "💡 Instale um deles:"
            echo "   ReportGenerator: dotnet tool install --global dotnet-reportgenerator-globaltool"
            echo "   Coverlet: dotnet tool install --global coverlet.console"
            exit 1
        fi
        ;;
esac

echo "✅ Relatório gerado em: CoverageReport/index.html"
echo "🌐 Para visualizar, abra: file://$(pwd)/CoverageReport/index.html"