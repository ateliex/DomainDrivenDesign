# Ateliex

# An�lise e Projeto

## Migrations

add-migration -name Initial -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows
update-database -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows
drop-database -project Ateliex.EntityFrameworkCore -startupproject Ateliex.Windows

### Nuget
- Microsoft.UI.Xaml

# Observa��es.
- Estilo reativo.
- Somente a camada de aplica��o e de dom�nio s�o modularizadas pelos conceitos; o restante � pelo vi�s t�cnico.
- Tentativa de manter altera��es agrupadas por vers�o: ao repopular o banco de dados de leitura fica dif�cil determinar as exclus�es.

# TODO
- Implementar WebSockets.