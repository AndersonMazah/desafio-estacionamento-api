# Estacionamento API

API REST desenvolvida em .NET 8 para cadastro de usuários, autenticação
com JWT e gerenciamento de veículos.

O projeto segue arquitetura em camadas e utiliza MediatR,
FluentValidation e Entity Framework Core com banco em memória.

---

# Tecnologias utilizadas

- .NET 8
- ASP.NET Core Web API com Controllers
- Entity Framework Core InMemory
- MediatR
- FluentValidation
- JWT Bearer Authentication
- Swagger / OpenAPI
- BCrypt (hash de senha)

---

# Estrutura da solução

``` text
src/estacionamento-api
├── Estacionamento.Application
├── Estacionamento.Domain
├── Estacionamento.Infrastructure
└── Estacionamento.WebApi
```

## Camadas:

### Application
- Contém serviços da aplicação, commands, queries e handlers com MediatR.

### Domain
- Contém entidades de domínio, enumeradores e interfaces de repositório.

### Infrastructure  
- Contém DbContext usando EF Core InMemory e implementações de repositório.

### WebApi
- Contém controllers, configuração de autenticação JWT e Swagger.

---

# Persistência de dados

A aplicação utiliza **Entity Framework Core InMemory**, e isso significa que:
- nenhum banco externo é necessário
- os dados são armazenados apenas em memória
- ao reiniciar a aplicação os dados são perdidos

---

# Como executar

## 1. Pré-requisitos

Ter instalado:
- .NET 8 SDK

Verifique com:

``` bash
dotnet --version
```

---

## 2. Restaurar dependências

Na raiz da solução execute:

``` bash
dotnet restore
```

---

## 3. Compilar o projeto

``` bash
dotnet build Estacionamento.sln
```

---

## 4. Executar a API

``` bash
dotnet run --project Estacionamento.WebApi
```

---

## 5. Acessar o Swagger

Com a API em execução, abra:

``` text
http://localhost:5215/swagger
```

---

# Autenticação

A API utiliza autenticação baseada em **JWT (JSON Web Token)**.

1.  cadastrar um usuário
2.  realizar login
3.  obter token JWT
4.  usar token para acessar endpoints protegidos

---

# Como cadastrar usuário

Endpoint:

POST /usuarios

Exemplo de JSON:

``` json
{
  "nome": "Anderson",
  "login": "anderson",
  "senha": "senha123"
}
```

---

# Como fazer login

Endpoint:

POST /auth/login

Exemplo de JSON:

``` json
{
  "login": "anderson",
  "senha": "senha123"
}
```

Resposta esperada:

``` json
{
  "token": "jwt-gerado"
}
```

---

# Como usar o token no Swagger

1.  cadastre um usuário
2.  faça login em POST /auth/login
3.  copie o valor do campo token
4.  no Swagger clique em Authorize
5.  informe o token no formato:

Bearer [TOKEN]

6.  confirme para liberar acesso aos endpoints protegidos

---

# Endpoints disponíveis

## Usuários

### POST /usuarios

Cadastrar usuário.

``` json
{
  "nome": "Anderson",
  "login": "anderson",
  "senha": "senha123"
}
```

---

## Autenticação

### POST /auth/login

Realiza login e retorna token JWT.

``` json
{
  "login": "anderson",
  "senha": "senha123"
}
```

---

## Veículos

Todos os endpoints exigem autenticação JWT.

### POST /veiculos

Cadastrar veículo.

``` json
{
  "descricao": "Carro de passeio",
  "marca": "Ford",
  "modelo": "Ka bolinha (2003)",
  "opcionais": "sem acessórios",
  "valor": 13999
}
```

---

### PUT /veiculos/{{id}}

Atualizar veículo.

``` json
{
  "descricao": "Carro de passeio",
  "marca": "Ford",
  "modelo": "Ka bolinha (2003)",
  "opcionais": "porta copo",
  "valor": 13999
}
```

---

### GET /veiculos/{{id}}

Consultar veículo por identificador.

---

### GET /veiculos

Listar veículos cadastrados.

---

### DELETE /veiculos/{{id}}

Remover veículo.

Retorna 204 No Content.

---
