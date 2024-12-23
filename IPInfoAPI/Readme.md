# IPInfoAPI

## Descri��o
IPInfoAPI � uma API REST que fornece informa��es detalhadas sobre endere�os IP, incluindo dados de geolocaliza��o e funcionalidades de atualiza��o peri�dica.

## Funcionalidades

1. **Endpoint de Informa��es de IP**
   - Retorna detalhes de um IP espec�fico (Nome do Pa�s, C�digo de Duas Letras, C�digo de Tr�s Letras).
   - Utiliza cache, banco de dados e servi�o IP2C para obter informa��es.

2. **Atualiza��o Peri�dica de Informa��es de IP**
   - Job em background que atualiza informa��es de IP a cada hora.
   - Processa IPs em lotes de 100.

3. **Endpoint de Relat�rio**
   - Fornece um relat�rio de endere�os por pa�s no banco de dados.
   - Inclui contagem de endere�os e �ltima atualiza��o por pa�s.

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Docker

## Configura��o

### Pr�-requisitos
- .NET SDK
- Docker

### Configura��o do Banco de Dados
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin@123" -p 1433:1433 --name ipinfo --hostname ipinfo -d mcr.microsoft.com/mssql/server:2022-latest

### Configura��o da Aplica��o
1. Clone o reposit�rio
2. Atualize a string de conex�o no `appsettings.json`
3. Iniciar app .net para iniciar conteiner
4. dotnet ef database update (usar localhost na string de conex�o)

## Estrutura do Projeto

- `Controllers/`: Controladores da API
- `Models/`: Modelos de dados
- `Services/`: Servi�os de neg�cios
- `Data/`: Contexto do banco de dados
- `BackgroundServices/`: Servi�os em segundo plano
- `DTOs/`: Objetos de Transfer�ncia de Dados