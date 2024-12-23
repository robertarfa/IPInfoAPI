# IPInfoAPI

## Descrição
IPInfoAPI é uma API REST que fornece informações detalhadas sobre endereços IP, incluindo dados de geolocalização e funcionalidades de atualização periódica.

## Funcionalidades

1. **Endpoint de Informações de IP**
   - Retorna detalhes de um IP específico (Nome do País, Código de Duas Letras, Código de Três Letras).
   - Utiliza cache, banco de dados e serviço IP2C para obter informações.

2. **Atualização Periódica de Informações de IP**
   - Job em background que atualiza informações de IP a cada hora.
   - Processa IPs em lotes de 100.

3. **Endpoint de Relatório**
   - Fornece um relatório de endereços por país no banco de dados.
   - Inclui contagem de endereços e última atualização por país.

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Docker

## Configuração

### Pré-requisitos
- .NET SDK
- Docker

### Configuração do Banco de Dados
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin@123" -p 1433:1433 --name ipinfo --hostname ipinfo -d mcr.microsoft.com/mssql/server:2022-latest

### Configuração da Aplicação
1. Clone o repositório
2. Atualize a string de conexão no `appsettings.json`
3. Iniciar app .net para iniciar conteiner
4. dotnet ef database update (usar localhost na string de conexão)

## Estrutura do Projeto

- `Controllers/`: Controladores da API
- `Models/`: Modelos de dados
- `Services/`: Serviços de negócios
- `Data/`: Contexto do banco de dados
- `BackgroundServices/`: Serviços em segundo plano
- `DTOs/`: Objetos de Transferência de Dados