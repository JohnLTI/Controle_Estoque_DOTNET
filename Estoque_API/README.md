# Estoque_API

Este é um projeto de API de Estoque desenvolvido em .NET 6 e utilizando o MySQL como banco de dados. A API oferece endpoints para gerenciar operações relacionadas ao estoque de produtos.

## Requisitos

Certifique-se de ter o seguinte instalado em seu ambiente de desenvolvimento:

- .NET 6 SDK
- MySQL Server

## Configuração do Banco de Dados

1. Crie um banco de dados no MySQL para armazenar os dados da aplicação.
2. Atualize a string de conexão no arquivo `appsettings.json` com as informações do seu banco de dados.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NomeDoSeuBancoDeDados;User=SeuUsuario;Password=SuaSenha;"
}
```

## Executando o Projeto

1. Clone este repositório.
2. Navegue até o diretório do projeto.

```bash
cd Estoque_API
```

3. Execute a aplicação usando o comando:

```bash
dotnet run
```

A API estará disponível em `https://localhost:5001` (ou `http://localhost:5000`).

## Endpoints

A API fornece os seguintes endpoints:

- `GET /api/produtos`: Retorna a lista de todos os produtos no estoque.
- `GET /api/produtos/{id}`: Retorna os detalhes de um produto específico.
- `POST /api/produtos`: Adiciona um novo produto ao estoque.
- `PUT /api/produtos/{id}`: Atualiza os detalhes de um produto existente.

Certifique-se de revisar a documentação detalhada da API para obter informações sobre os payloads de solicitação e resposta.

## Contribuindo

- [@EmmanuelMartins21](https://github.com/EmmanuelMartins21) ➡ [LinkedIn](https://www.linkedin.com/in/emmanuel-cosme-martins-bento-3963bb1b9/)
