┌─────────────────────────────────────────────────────────┐
│ ** PRESENTATION (Controllers/API)                       │
│ • Receber requisições HTTP                              │
│ • Validar dados de entrada                              │
│ • Serializar/Deserializar JSON                          │
│ • Retornar códigos de status HTTP                       │
└─────────────────────┬───────────────────────────────────┘
                      ▼
┌─────────────────────────────────────────────────────────┐
│ ** APPLICATION (Services/Use Cases)                     │
│ • Orquestrar fluxo de negócio                           │
│ • Coordenar Domain e Infrastructure                     │
│ • Implementar casos de uso                              │
│ • Gerenciar transações                                  │
└─────────────────────┬───────────────────────────────────┘
                      ▼
┌─────────────────────────────────────────────────────────┐
│ ** DOMAIN (Entities/Business Rules) - NÚCLEO            │
│ • Regras de negócio essenciais                          │
│ • Entidades e Value Objects                             │
│ • Domain Services                                       │
│ • Interfaces (contratos)                                │
│ • Exceções de domínio                                   │
└─────────────────────▲───────────────────────────────────┘
                      │
┌─────────────────────────────────────────────────────────┐
│ ** INFRASTRUCTURE (Data/External)                       │
│ • Acesso a banco de dados                               │
│ • Chamadas para APIs externas                           │
│ • Sistema de arquivos                                   │
│ • Implementação de interfaces do Domain                 │
└─────────────────────────────────────────────────────────┘

🎯 Regra de Ouro
Dependências sempre apontam PARA DENTRO:
Presentation = conhece Application e Domain
Application = conhece apenas Domain
Domain = independente (não conhece ninguém)
Infrastructure = conhece apenas Domain
