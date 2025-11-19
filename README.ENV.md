# Configuração de Variáveis de Ambiente

Este projeto usa variáveis de ambiente para configurações sensíveis, seguindo as melhores práticas de segurança.

## Setup Inicial

1. **Copie o arquivo de exemplo:**
   ```bash
   cp .env.example .env
   ```

2. **Edite o arquivo `.env` com suas configurações:**
   ```bash
   nano .env
   # ou
   vim .env
   ```

## Variáveis Obrigatórias

### ConnectionStrings__DefaultConnection
String de conexão do banco de dados SQL Server.
```
ConnectionStrings__DefaultConnection=Server=tcp:seu-servidor.database.windows.net,1433;Initial Catalog=seu-banco;Persist Security Info=False;User ID=seu-usuario;Password=sua-senha;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

### Jwt__Key
Chave secreta para assinatura dos tokens JWT. Deve ter no mínimo 32 caracteres. Recomenda-se usar uma chave gerada aleatoriamente.
```
Jwt__Key=sua-chave-secreta-aqui-minimo-32-caracteres
```

## Variáveis Opcionais

### Jwt__Issuer
Emissor do token JWT. Padrão: `GoalRoadApi`
```
Jwt__Issuer=GoalRoadApi
```

### Jwt__Audience
Audiência do token JWT. Padrão: `GoalRoadClient`
```
Jwt__Audience=GoalRoadClient
```

### Jwt__ExpiresMinutes
Tempo de expiração do token em minutos. Padrão: `120`
```
Jwt__ExpiresMinutes=120
```

### Logging__LogLevel__Default
Nível de log padrão. Padrão: `Information`
```
Logging__LogLevel__Default=Information
```

### Logging__LogLevel__Microsoft.AspNetCore
Nível de log do ASP.NET Core. Padrão: `Warning`
```
Logging__LogLevel__Microsoft.AspNetCore=Warning
```

### AllowedHosts
Hosts permitidos. Padrão: `*`
```
AllowedHosts=*
```

### ASPNETCORE_ENVIRONMENT
Ambiente de execução. Padrão: `Development`
```
ASPNETCORE_ENVIRONMENT=Development
```

### ASPNETCORE_URLS
URLs onde a aplicação escutará. Padrão: `http://+:8080`
```
ASPNETCORE_URLS=http://+:8080
```

## Formato das Variáveis

O ASP.NET Core usa o formato `__` (dois underscores) para representar hierarquia de configuração:

- `ConnectionStrings__DefaultConnection` → `ConnectionStrings.DefaultConnection`
- `Jwt__Key` → `Jwt.Key`
- `Logging__LogLevel__Default` → `Logging.LogLevel.Default`

## Uso com Docker

O docker-compose já está configurado para ler o arquivo `.env` automaticamente:

```bash
docker-compose up -d
```

## Uso Local (sem Docker)

Para desenvolvimento local, você pode:

1. **Usar o arquivo `.env` diretamente** (o ASP.NET Core lê variáveis de ambiente automaticamente)

2. **Exportar variáveis no terminal:**
   ```bash
   export ConnectionStrings__DefaultConnection="sua-connection-string"
   export Jwt__Key="sua-chave"
   dotnet run --project GoalRoad/GoalRoad.csproj
   ```

3. **Usar um gerenciador de variáveis de ambiente** como `dotenv` ou configurar no seu IDE

## Segurança

⚠️ **IMPORTANTE:**
- Nunca commite o arquivo `.env` no Git
- O arquivo `.env` já está no `.gitignore`
- Use `.env.example` como template para outros desenvolvedores
- Em produção, use serviços de gerenciamento de secrets (Azure Key Vault, AWS Secrets Manager, etc.)

## Geração de Chave JWT

Para gerar uma chave segura para JWT:

```bash
# Linux/Mac
openssl rand -hex 64

# PowerShell (Windows)
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))
```


