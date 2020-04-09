# Ateliex

# Análise e Projeto

## Migrations

add-migration -name Initial -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows
update-database -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows
drop-database -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows

### Nuget
- Microsoft.UI.Xaml

# Observações.
- Estilo reativo.
- Somente a camada de aplicação e de domínio são modularizadas pelos conceitos; o restante é pelo viés técnico.
- Tentativa de manter alterações agrupadas por versão: ao repopular o banco de dados de leitura fica difícil determinar as exclusões.

# TODO
- Implementar WebSockets.