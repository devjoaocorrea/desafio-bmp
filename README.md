# Desafio BMP

Este repositório contém uma aplicação de desafio para a empresa BMP, desenvolvida com .NET e C#. O projeto adota práticas de Clean Architecture e segue padrões de desenvolvimento de software.

## Tecnologias

- .NET 6.0
- Entity Framework Core
- FluentValidation
- Docker/Docker Compose

## Estrutura do Projeto

- `.Api`: Contém a API principal.
- `.App`: Injeções de dependências.
- `.Domain`: Contém as Entidades (modelos), Interfaces e Handlers.
- `.Dto`: (Data transfer objects) Commands e responses.
- `.Infra`: Infraestrutura do projeto, incluindo o contexto do banco de dados.
- `.Tests`: É onde fica os testes unitários.

## Configuração

### Rodando com Docker

1. Certifique-se de que Docker e Docker Compose estão instalados.
   ```
   docker --version
   docker-compose --version
   ```
2. Caso não tenha, pode baixá-lo por [aqui](https://docs.docker.com/get-started/get-docker/)
3. Clone o repositório
4. No diretório raiz do projeto, execute:
   ```
   docker-compose up --build
   ```
5. A aplicação estará disponível em http://localhost:5000/swagger ou https://localhost:5001/swagger
6. A porta padrão configurada é 5000 (http) e 5001 (https) você pode configurar a porta que deseja nos arquivos `.yml`.

### Rodando localmente

1. Clone o repositório
2. Configure o banco de dados no appsettings.json
3. Execute os comandos:
   ```
   dotnet restore desafio-bmp.sln
   dotnet run --project ./BancoChu.Api
   ```

### Testes

Para rodar os testes, utilize:
```
dotnet test ./BancoChu.Tests
```
