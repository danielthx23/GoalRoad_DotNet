# Docker Setup para GoalRoad

Este projeto inclui configurações Docker para facilitar o desenvolvimento e deploy.

## Arquivos Docker

- `Dockerfile` - Dockerfile de produção (multi-stage build)
- `Dockerfile.dev` - Dockerfile para desenvolvimento com hot reload
- `docker-compose.yml` - Configuração para produção
- `docker-compose.dev.yml` - Configuração para desenvolvimento
- `.dockerignore` - Arquivos ignorados no build

## Como usar

### Produção

1. **Build da imagem:**
   ```bash
   docker build -t goalroad-api:latest .
   ```

2. **Executar com docker-compose:**
   ```bash
   docker-compose up -d
   ```

3. **Executar manualmente:**
   ```bash
   docker run -d \
     -p 8080:8080 \
     -e ConnectionStrings__DefaultConnection="sua-connection-string" \
     -e Jwt__Key="sua-jwt-key" \
     -v $(pwd)/GoalRoad/Treinamento:/app/Treinamento \
     --name goalroad-api \
     goalroad-api:latest
   ```

### Desenvolvimento

1. **Executar com docker-compose (hot reload):**
   ```bash
   docker-compose -f docker-compose.dev.yml up
   ```

2. **Build e executar manualmente:**
   ```bash
   docker build -f Dockerfile.dev -t goalroad-api:dev .
   docker run -it --rm \
     -p 8080:8080 \
     -v $(pwd):/src \
     -v $(pwd)/GoalRoad/Treinamento:/app/Treinamento \
     goalroad-api:dev
   ```

## Variáveis de Ambiente

As configurações sensíveis foram movidas para variáveis de ambiente. Crie um arquivo `.env` na raiz do projeto baseado no `.env.example`:

```bash
cp .env.example .env
```

Edite o arquivo `.env` com suas configurações:

- `ConnectionStrings__DefaultConnection` - String de conexão do SQL Server (obrigatório)
- `Jwt__Key` - Chave secreta para JWT (obrigatório, mínimo 32 caracteres)
- `Jwt__Issuer` - Emissor do JWT (padrão: GoalRoadApi)
- `Jwt__Audience` - Audiência do JWT (padrão: GoalRoadClient)
- `Jwt__ExpiresMinutes` - Tempo de expiração do token (padrão: 120)
- `Logging__LogLevel__Default` - Nível de log padrão (padrão: Information)
- `Logging__LogLevel__Microsoft.AspNetCore` - Nível de log do ASP.NET (padrão: Warning)
- `AllowedHosts` - Hosts permitidos (padrão: *)
- `ASPNETCORE_ENVIRONMENT` - Ambiente (Development/Production)
- `ASPNETCORE_URLS` - URLs da aplicação (padrão: http://+:8080)

**Importante:** O arquivo `.env` não deve ser commitado no Git. Use `.env.example` como template.

## Volumes

O diretório `GoalRoad/Treinamento` é montado como volume para persistir os modelos ML.NET treinados.

## Health Check

O container inclui um health check que verifica o endpoint `/health` a cada 30 segundos.

## Migrations

Para executar migrations do Entity Framework, você pode:

1. **Dentro do container:**
   ```bash
   docker exec -it goalroad-api dotnet ef database update --project GoalRoad.Infrastructure
   ```

2. **Ou criar um script de inicialização** que execute as migrations automaticamente no startup.

## Portas

- `8080` - HTTP (API)
- `8081` - HTTPS (se configurado)

## Troubleshooting

### Ver logs do container:
```bash
docker logs goalroad-api
```

### Acessar o container:
```bash
docker exec -it goalroad-api bash
```

### Rebuild completo:
```bash
docker-compose down
docker-compose build --no-cache
docker-compose up -d
```

