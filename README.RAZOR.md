# Páginas Razor - GoalRoad

Este documento explica como acessar e usar as páginas Razor do projeto GoalRoad.

## 🚀 Como Acessar as Páginas

### 1. Iniciar a Aplicação

```bash
# Desenvolvimento
dotnet run --project GoalRoad/GoalRoad.csproj

# Ou com Docker
docker-compose up
```

A aplicação estará disponível em: **http://localhost:5000** ou **http://localhost:8080** (dependendo da configuração)

### 2. URLs das Páginas

Todas as páginas Razor estão disponíveis através das seguintes URLs:

| Página | URL | Descrição |
|--------|-----|-----------|
| **Home** | `/` ou `/Index` | Página inicial com visão geral do sistema |
| **Usuários** | `/Usuarios` | Lista de todos os usuários cadastrados |
| **Carreiras** | `/Carreiras` | Lista de carreiras disponíveis |
| **Tecnologias** | `/Tecnologias` | Lista de tecnologias cadastradas |
| **Categorias** | `/Categorias` | Lista de categorias de carreiras |
| **Feed** | `/Feed` | Visualização dos feeds personalizados dos usuários |

### 3. Estrutura de Navegação

A aplicação possui uma barra de navegação no topo com links para todas as páginas principais:

- 🏠 **Home** - Página inicial
- 👥 **Usuários** - Gerenciamento de usuários
- 💼 **Carreiras** - Gerenciamento de carreiras
- 💻 **Tecnologias** - Gerenciamento de tecnologias
- 🏷️ **Categorias** - Gerenciamento de categorias
- 📰 **Feed** - Visualização de feeds
- 📚 **API Docs** - Link para Swagger UI

## 📋 Funcionalidades das Páginas

### Página Home (`/`)
- Dashboard com visão geral do sistema
- Cards de navegação rápida para cada seção
- Design moderno com Bootstrap 5
- Ícones Bootstrap Icons

### Página Usuários (`/Usuarios`)
- Lista todos os usuários cadastrados
- Exibe: ID, Nome, Email e Data de Criação
- Tabela responsiva com Bootstrap
- Badge com total de registros

### Página Carreiras (`/Carreiras`)
- Lista todas as carreiras disponíveis
- Exibe: ID, Título, Descrição e Categoria
- Descrições truncadas para melhor visualização
- Badge para categoria

### Página Tecnologias (`/Tecnologias`)
- Lista todas as tecnologias cadastradas
- Exibe: ID, Nome e Descrição
- Layout limpo e organizado

### Página Categorias (`/Categorias`)
- Lista todas as categorias de carreiras
- Exibe: ID, Nome e Descrição
- Interface consistente com outras páginas

### Página Feed (`/Feed`)
- Visualização em cards dos feeds dos usuários
- Exibe: ID do usuário, quantidade de itens
- Prévia dos últimos 3 itens de cada feed
- Layout em grid responsivo

## 🎨 Design e Estilo

As páginas utilizam:
- **Bootstrap 5.3.0** - Framework CSS moderno
- **Bootstrap Icons** - Ícones consistentes
- **Layout Responsivo** - Adaptável a diferentes tamanhos de tela
- **Cards e Tabelas** - Componentes Bootstrap estilizados
- **Cores Temáticas** - Cada seção tem sua cor característica

## 🔧 Estrutura de Arquivos

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
        └── site.css              # Estilos customizados
```

## 📝 Notas Importantes

1. **Razor Pages vs MVC**: Este projeto usa Razor Pages, que é mais simples que MVC para páginas simples
2. **Roteamento**: As páginas são acessadas diretamente pela URL baseada no nome da pasta
3. **Layout Compartilhado**: Todas as páginas usam o mesmo layout (`_Layout.cshtml`)
4. **Dados**: As páginas consomem dados através dos Use Cases da camada de aplicação

## 🐛 Troubleshooting

### Página não encontrada (404)
- Verifique se o servidor está rodando
- Confirme que `app.MapRazorPages()` está no `Program.cs`
- Verifique se `services.AddRazorPages()` está no `DependencyInjection.cs`

### Estilos não carregam
- Verifique se os arquivos estáticos estão sendo servidos
- Confirme que o Bootstrap CDN está acessível
- Verifique o console do navegador para erros

### Dados não aparecem
- Verifique a conexão com o banco de dados
- Confirme que há dados no banco
- Verifique os logs da aplicação

## 🔗 Links Úteis

- **Swagger UI**: `/swagger` - Documentação da API
- **Health Check**: `/health` - Status da aplicação
- **API Base**: `/api/[controller]` - Endpoints da API REST


