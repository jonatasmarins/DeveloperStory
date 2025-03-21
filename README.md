# DeveloperStore API

A API de e-commerce para gerenciamento de produtos, categorias e transações de venda. Esta API foi construída utilizando .NET Core e fornece endpoints RESTful para interagir com os dados dos produtos e outros recursos da loja.

## Funcionalidades

- Gerenciamento de produtos (criação, leitura, atualização e exclusão).
- Obtenção de produtos por categoria.
- Paginação de resultados para facilitar a navegação.
- Autenticação de usuários (caso necessário).
  
## Tecnologias Utilizadas

- **.NET Core 8.0**
- **Entity Framework Core** para acesso a banco de dados.
- **Postgresql** para o armazenamento de dados.
- **Swagger** para documentação da API.
- **FluentValidation** para validação de dados de entrada.
- **Xunit** para testes de unidade.
- **Bogus** para mock de informação.
- **NSubistitute** para mock de classe e injeção de dependência.

![.NET Badge](https://img.shields.io/badge/.NET-8.0-purple)
![PostgreSQL Badge](https://img.shields.io/badge/PostgreSQL-13.0-blue?logo=postgresql&logosize=large&logoColor=white)
  
## Pré-requisitos

Antes de começar, certifique-se de que você tem os seguintes pré-requisitos instalados:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- **DBeaver** ou qualquer outra IDE compatível com Postgresql.
- **Docker** (https://www.docker.com/get-started/).
  
## Como Rodar o Projeto

### 1. Clone o repositório

```bash

git clone https://github.com/jonatasmarins/developer-story.git

```

### 2. Execute o Docker Compose

```bash

docker-compose up -d

```

### 3. Execute a migration

É necessário executar a migration do entity framework para criar a estrutura do banco de dados

```bash
 cd src

 dotnet ef migrations add InitialCommit --project ./DeveloperStore.Infra --startup-project ./DeveloperStore.API

 dotnet ef database update --project ./DeveloperStore.Infra/  --startup-project ./DeveloperStore.API

```

## Estrutura do repositório

```
project/
├── src/
│   ├── API
│   ├── Domain
│   ├── Infra
│   └── App
├── test/
│   ├── Tests
└── docker-compose.yml
└── README.md

```
