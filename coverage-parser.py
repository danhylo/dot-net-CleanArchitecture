#!/usr/bin/env python3
"""
📊 Parser Simples de Cobertura XML para HTML
Alternativa gratuita ao ReportGenerator
"""

import xml.etree.ElementTree as ET
import os
import glob
from datetime import datetime

def parse_coverage_xml():
    # Encontrar arquivo de cobertura
    coverage_files = glob.glob("TestResults/*/coverage.cobertura.xml")
    if not coverage_files:
        print("❌ Arquivo de cobertura não encontrado")
        return
    
    coverage_file = coverage_files[0]
    print(f"📄 Processando: {coverage_file}")
    
    # Parse XML
    tree = ET.parse(coverage_file)
    root = tree.getroot()
    
    # Extrair dados
    line_rate = float(root.get('line-rate', 0)) * 100
    branch_rate = float(root.get('branch-rate', 0)) * 100
    timestamp = root.get('timestamp', str(int(datetime.now().timestamp())))
    
    # Gerar HTML simples
    html_content = f"""
<!DOCTYPE html>
<html>
<head>
    <title>📊 Relatório de Cobertura</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; }}
        .header {{ background: #f0f0f0; padding: 20px; border-radius: 8px; }}
        .metric {{ display: inline-block; margin: 10px; padding: 15px; background: #e8f4fd; border-radius: 5px; }}
        .high {{ background: #d4edda; }}
        .medium {{ background: #fff3cd; }}
        .low {{ background: #f8d7da; }}
        table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
        th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
        th {{ background-color: #f2f2f2; }}
    </style>
</head>
<body>
    <div class="header">
        <h1>📊 Relatório de Cobertura de Testes</h1>
        <p>Gerado em: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}</p>
    </div>
    
    <div class="metric {'high' if line_rate >= 80 else 'medium' if line_rate >= 60 else 'low'}">
        <h3>📈 Cobertura de Linhas</h3>
        <h2>{line_rate:.1f}%</h2>
    </div>
    
    <div class="metric {'high' if branch_rate >= 80 else 'medium' if branch_rate >= 60 else 'low'}">
        <h3>🌿 Cobertura de Branches</h3>
        <h2>{branch_rate:.1f}%</h2>
    </div>
    
    <h2>📋 Detalhes por Classe</h2>
    <table>
        <tr>
            <th>Classe</th>
            <th>Linhas Cobertas</th>
            <th>Total de Linhas</th>
            <th>Cobertura %</th>
        </tr>
"""
    
    # Processar classes
    for package in root.findall('.//package'):
        for class_elem in package.findall('.//class'):
            class_name = class_elem.get('name', 'Unknown')
            lines = class_elem.findall('.//line')
            
            if lines:
                covered_lines = sum(1 for line in lines if int(line.get('hits', 0)) > 0)
                total_lines = len(lines)
                coverage_percent = (covered_lines / total_lines * 100) if total_lines > 0 else 0
                
                color_class = 'high' if coverage_percent >= 80 else 'medium' if coverage_percent >= 60 else 'low'
                
                html_content += f"""
        <tr class="{color_class}">
            <td>{class_name.split('.')[-1]}</td>
            <td>{covered_lines}</td>
            <td>{total_lines}</td>
            <td>{coverage_percent:.1f}%</td>
        </tr>"""
    
    html_content += """
    </table>
    
    <footer style="margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #666;">
        <p>🐍 Gerado por coverage-parser.py - Alternativa gratuita ao ReportGenerator</p>
    </footer>
</body>
</html>
"""
    
    # Salvar HTML
    os.makedirs("CoverageReport", exist_ok=True)
    with open("CoverageReport/index.html", "w") as f:
        f.write(html_content)
    
    print(f"✅ Relatório HTML gerado: CoverageReport/index.html")
    print(f"📈 Cobertura de Linhas: {line_rate:.1f}%")
    print(f"🌿 Cobertura de Branches: {branch_rate:.1f}%")

if __name__ == "__main__":
    parse_coverage_xml()