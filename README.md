# GoalRoad (Entrega)

---

## Sumário

***Em negrito são os tópicos importantes para entrega***

- [Descrição do Projeto](#descrição-do-projeto)
- [Autores](#autores)
- [Vídeo de Demonstração](#vídeo-de-demonstração)
- [Arquitetura Atual](#arquitetura-atual)
- [Arquitetura Futura](#arquitetura-futura)
- [Instalação via Docker / Host / Local](#instalação-via-docker--host--local)
  - [Requisitos](#requisitos)
  - [Rodar Localmente (Host)](#rodar-localmente-host)
  - [Rodar com Docker (imagem leve)](#rodar-com-docker-imagem-leve)
- [Acesso à API](#acesso-à-api)
- [Exemplos de Teste da API](#exemplos-de-teste-da-api)
  - [Carreira](#carreira)
  - [RoadMap](#roadmap)
  - [Tecnologia](#tecnologia)
  - [RoadMapTecnologia](#roadmaptecnologia)
  - [Feed](#feed)
  - [FeedItem](#feeditem)
  - [Usuário](#usuário)
  - [Auth](#auth)
  - [Health](#health)
- [Rotas da API (mapeamento dos Controllers)](#rotas-da-api-mapeamento-dos-controllers)
- [O que o código cobre dos requisitos (resumo)](#o-que-o-código-cobre-dos-requisitos-resumo)

---

## Descrição do Projeto

GoalRoad é uma API RESTful em .NET que fornece operações para gerenciar carreiras, roadmaps, tecnologias, feeds e usuários — focada em demonstrar boas práticas de APIs (paginação, versionamento, health checks, autenticação, persistência com EF Core e testes).

Esta README foi produzida para entregar o checkpoint e mapear claramente os endpoints existentes e quais requisitos estão cobertos pelo código.

---

## Autores

- Daniel Saburo Akiyama
- Outros colaboradores (ver `GoalRoad.sln`)

---

## Vídeo de Demonstração

[![Thumbnail](https://img.youtube.com/vi/-xJgy2ZP8yE/0.jpg)](https://www.youtube.com/watch?v=-xJgy2ZP8yE)

## Link Deploy

[https://goalroad-dotnet.onrender.com](https://goalroad-dotnet.onrender.com)

---

## Arquitetura Atual

Aplicação monolítica ASP.NET Core com camadas separadas (Presentation, Application, Infrastructure). EF Core é usado para persistência; controllers expõem a API em `api/[controller]`.

---

## Arquitetura Futura

Sugestões: adicionar orquestração (Docker Compose / Kubernetes), separar serviços em microserviços, adicionar pipeline CI/CD e monitoramento centralizado (Jaeger/Prometheus/Grafana).

---

## Instalação via Docker / Host / Local

### Requisitos

- .NET 8 SDK (para rodar localmente)
- Docker (opcional)
- SQL Server ou usar fallback in-memory (o projeto cai para InMemory se não houver connection string)

### Rodar Localmente (Host)

1. Copie `.env.example` para `.env` (ou configure `appsettings.Development.json`) com a string de conexão `ConnectionStrings:DefaultConnection` apontando para um SQL Server.

2. Restaurar e rodar:

```bash
dotnet restore
dotnet build
dotnet run --project GoalRoad --urls "http://localhost:5024"
```

> Observação: se preferir usar o banco em memória para desenvolvimento, deixe `ASPNETCORE_ENVIRONMENT` como `Development` e não forneça `ConnectionStrings__DefaultConnection`.

### Rodar com Docker (imagem leve)

- Build e run em uma imagem (exemplo genérico):

```bash
docker build -t goalroad-api:latest .
docker run -d -p 5024:5024 -e Jwt__Key="sua-chave-secreta-min-32" -e ConnectionStrings__DefaultConnection="<sua-connection-string>" --name goalroad-api goalroad-api:latest
```

---

## Acesso à API

- Aplicação Razor (se habilitada): `http://localhost:5024/`
- Swagger: `http://localhost:5024/swagger`
- Base API: `http://localhost:5024/api/`

---

## Exemplos de Teste da API

Abaixo há exemplos de payloads usados pelos endpoints (JSON). Muitos endpoints suportam paginação via query params `offset` e `limit` (padrão: 0,10).

### Carreira

POST /api/Carreira
```json
{
  "nmCarreira": "Engenharia de Software",
  "dsObjetivo": "Roadmap para se tornar desenvolvedor"
}
```

PUT /api/Carreira/{id}
```json
{
  "idCarreira": 1,
  "nmCarreira": "Engenharia de Software Atualizada",
  "dsObjetivo": "..."
}
```

### RoadMap

POST /api/RoadMap
```json
{
  "idCarreira": 1,
  "nmTitulo": "Roadmap Back-End",
  "dsDescricao": "..."
}
```

### Tecnologia

POST /api/Tecnologia
```json
{
  "idTecnologia": 0,
  "nmTecnologia": "C#",
  "dsDescricao": "Linguagem principal"
}
```

### RoadMapTecnologia (associação)

POST /api/RoadMapTecnologia
```json
{
  "idRoadMap": 1,
  "idTecnologia": 2
}
```

### Feed

POST /api/Feed
```json
{
  "idUsuario": 1,
  "nmTitulo": "Nova vaga",
  "nmConteudo": "..."
}
```

POST /api/Feed/generate/{userId}/{carreiraId}?top=20
- Gera e persiste o feed do usuário a partir de um roadmap.

### FeedItem

POST /api/FeedItem
```json
{
  "id": 0,
  "nmConteudo": "Item do feed",
  "feedId": 1
}
```

### Usuário

POST /api/Usuario
```json
{
  "nmUsuario": "joaosilva",
  "senha": "SenhaForte123",
  "email": "joao@exemplo.com"
}
```

### Auth

POST /api/Auth/login
```json
{
  "email": "joao@exemplo.com",
  "senha": "SenhaForte123"
}
```

### Health

- Liveness: `GET /api/Health/live`
- Readiness: `GET /api/Health/ready`
- Health geral: `GET /health` (mapeado no `Program.cs` com detalhes)

---

## Rotas da API (mapeamento dos Controllers)

Observação: todos os controllers usam `[Route("api/[controller]")]`. Abaixo estão as rotas existentes por controller (extraídas do código):

### CarreiraController (GoalRoad/Controllers/CarreiraController.cs)
- GET    /api/Carreira?offset={offset}&limit={limit}    — Lista paginada (offset, limit)
- GET    /api/Carreira/all                             — Lista completa
- GET    /api/Carreira/{id}                            — Obter por id
- POST   /api/Carreira                                 — Criar (201 Created)
- PUT    /api/Carreira/{id}                            — Atualizar (400 se id mismatch)
- DELETE /api/Carreira/{id}                            — Deletar

### UsuarioController (GoalRoad/Controllers/UsuarioController.cs)
- GET    /api/Usuario?offset={offset}&limit={limit}
- GET    /api/Usuario/all
- GET    /api/Usuario/{id}
- POST   /api/Usuario
- PUT    /api/Usuario/{id}
- DELETE /api/Usuario/{id}

### HealthController (GoalRoad/Controllers/HealthController.cs)
- GET /api/Health/live   — Liveness (tags: "live")
- GET /api/Health/ready  — Readiness (tags: "ready")
- Além disso, `Program.cs` mapeia `/health` com o relatório completo.

### RoadMapTecnologiaController (GoalRoad/Controllers/RoadMapTecnologiaController.cs)
- GET    /api/RoadMapTecnologia?offset={offset}&limit={limit}
- GET    /api/RoadMapTecnologia/{id1}/{id2}
- POST   /api/RoadMapTecnologia
- PUT    /api/RoadMapTecnologia/{id1}/{id2}
- DELETE /api/RoadMapTecnologia/{id1}/{id2}

### FeedController (GoalRoad/Controllers/FeedController.cs)
- GET    /api/Feed?offset={offset}&limit={limit}
- GET    /api/Feed/{id}
- POST   /api/Feed
- PUT    /api/Feed/{id}
- DELETE /api/Feed/{id}
- POST   /api/Feed/generate/{userId}/{carreiraId}?top={top}   — Gera e persiste feed
- GET    /api/Feed/treinar                                   — Treina modelo ML.NET

### FeedItemController (GoalRoad/Controllers/FeedItemController.cs)
- GET    /api/FeedItem?offset={offset}&limit={limit}
- GET    /api/FeedItem/{id}
- POST   /api/FeedItem
- PUT    /api/FeedItem/{id}
- DELETE /api/FeedItem/{id}

### RoadMapController (GoalRoad/Controllers/RoadMapController.cs)
- GET    /api/RoadMap?offset={offset}&limit={limit}
- GET    /api/RoadMap/all
- GET    /api/RoadMap/{id}
- POST   /api/RoadMap
- PUT    /api/RoadMap/{id}
- DELETE /api/RoadMap/{id}

### TecnologiaController (GoalRoad/Controllers/TecnologiaController.cs)
- GET    /api/Tecnologia?offset={offset}&limit={limit}
- GET    /api/Tecnologia/all
- GET    /api/Tecnologia/{id}
- POST   /api/Tecnologia
- PUT    /api/Tecnologia/{id}
- DELETE /api/Tecnologia/{id}

### AuthController (GoalRoad/Controllers/AuthController.cs)
- POST /api/Auth/login — Autentica usuário e retorna token JWT (ou null → 401)

---

## Páginas Razor (mapeamento)

A aplicação expõe páginas Razor localizadas em `GoalRoad/Pages`. Estas páginas são usadas pela interface Razor (se habilitada) e consomem os use cases da camada de aplicação.

Tabela resumida das páginas e suas URLs:

| Página | URL | Descrição |
|--------|-----|-----------|
| Home | `/` ou `/Index` | Página inicial com visão geral do sistema |
| Usuários | `/Usuarios` | Lista e gestão de usuários |
| Carreiras | `/Carreiras` | Lista e gestão de carreiras |
| Tecnologias | `/Tecnologias` | Lista e gestão de tecnologias |
| Categorias | `/Categorias` | Lista e gestão de categorias |
| Feed | `/Feed` | Visualização dos feeds personalizados dos usuários |

Estrutura de arquivos (resumo):

```
GoalRoad/
├── Pages/
│   ├── Index.cshtml              # Página Home
│   ├── Index.cshtml.cs
│   ├── _ViewStart.cshtml         # Layout padrão
│   ├── _ViewImports.cshtml       # Imports globais
│   ├── Shared/
│   │   └── _Layout.cshtml        # Layout principal
│   ├── Usuarios/
│   │   ├── Index.cshtml
│   │   └── Index.cshtml.cs
│   ├── Carreiras/
│   │   ├── Index.cshtml
│   │   └── Index.cshtml.cs
│   ├── Tecnologias/
│   │   ├── Index.cshtml
│   │   └── Index.cshtml.cs
│   ├── Categorias/
│   │   ├── Index.cshtml
│   │   └── Index.cshtml.cs
│   └── Feed/
│       ├── Index.cshtml
│       └── Index.cshtml.cs
└── wwwroot/
    └── css/
        └── site.css
```

Notas importantes sobre as páginas Razor:
- As páginas usam `services.AddRazorPages()` e são mapeadas com `app.MapRazorPages()` no `Program.cs`.
- A navegação principal está no `_Layout.cshtml` (link para Home, Usuários, Carreiras, Tecnologias, Categorias, Feed e Swagger).
- Se os dados não aparecem, verifique a string de conexão e se as migrations foram aplicadas.

---

## Configuração de Variáveis de Ambiente

Este projeto usa variáveis de ambiente para configurações sensíveis. Copie ` .env.example` para ` .env` e personalize as chaves.

Principais variáveis (exemplos):

- `ConnectionStrings__DefaultConnection` — String de conexão SQL Server. Exemplo:

```
ConnectionStrings__DefaultConnection=Server=tcp:seu-servidor.database.windows.net,1433;Initial Catalog=seu-banco;Persist Security Info=False;User ID=seu-usuario;Password=sua-senha;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

- `Jwt__Key` — Chave secreta para JWT (mínimo 32 caracteres):

```
Jwt__Key=sua-chave-secreta-aqui-minimo-32-caracteres
```

- `Jwt__Issuer` — Emissor do token (opcional, padrão `GoalRoadApi`)
- `Jwt__Audience` — Audiência do token (opcional, padrão `GoalRoadClient`)
- `Jwt__ExpiresMinutes` — Tempo de expiração em minutos (padrão: `120`)

Outras variáveis úteis:

- `Logging__LogLevel__Default` (ex: `Information`)
- `Logging__LogLevel__Microsoft.AspNetCore` (ex: `Warning`)
- `AllowedHosts` (ex: `*`)
- `ASPNETCORE_ENVIRONMENT` (ex: `Development`)
- `ASPNETCORE_URLS` (ex: `http://+:8080`)

Boas práticas:
- Nunca commite o arquivo `.env`; use `.env.example` como template.
- Em produção, prefira serviços de gerenciamento de secrets (Key Vault, Secrets Manager).

Como gerar uma chave forte para `Jwt__Key`:

```bash
# Linux/Mac
openssl rand -hex 64

# PowerShell (Windows)
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))
```

---
