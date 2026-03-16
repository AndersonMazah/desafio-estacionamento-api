# Este documento contém minhas anotações referentes a como interpretei a leitura do desafio.

---

## Objetivo do desafio

Desenvolver uma API REST em .NET para cadastro e consulta de veículos, contendo também cadastro e autenticação de usuários.

O desafio tem como foco avaliar:

- Arquitetura da solução
- Organização do código
- Uso de boas práticas
- Uso correto das tecnologias solicitadas

---

## Requisitos Funcionais

### Usuários

- Cadastro de usuário
- Autenticação de usuário (login)
- Geração de token JWT

### Veículos

Operações protegidas por JWT:

- Cadastrar veículo
- Atualizar veículo
- Consultar veículo por Id
- Listar veículos
- Remover veículo

---

## Requisitos Não Funcionais

- Arquitetura em camadas
- Uso de MediatR com Commands e Queries
- Uso de FluentValidation para validação
- Autenticação baseada em JWT
- Controllers apenas como camada de entrada HTTP
- Regras de negócio fora dos controllers

---

## Arquitetura esperada

A solução deve ser organizada nas seguintes camadas:

### WebApi

Responsável pela exposição HTTP da aplicação.

Contém:

- Controllers
- Configuração de autenticação JWT
- Swagger / OpenAPI

Controllers esperados:

- AuthController
- UsuariosController
- VeiculosController

---

### Application

Responsável pela lógica de aplicação.

Contém:

- Services
- Commands
- Queries
- Handlers (MediatR)

Responsabilidades:

- Orquestrar regras de negócio
- Executar comandos
- Consultar dados via repositórios

---

### Domain

Responsável pelas entidades e contratos do sistema.

Contém:

- Entidades
- Enumeradores
- Interfaces de repositório

Entidades principais:

- Usuario
- Veiculo

Enumeradores:

- Marca

Interfaces:

- IUsuarioRepository
- IVeiculoRepository

---

### Infrastructure

Responsável pela persistência.

Contém:

- DbContext
- Implementação dos repositórios

Persistência definida no desafio:

Entity Framework Core com **InMemory**.

Isso significa que:

- não existe banco externo
- os dados são mantidos apenas durante a execução da aplicação

---

## Entidades do domínio

### Usuario

Campos obrigatórios:

- Id
- Nome
- Login
- Senha

Regras:

- Nome mínimo de 3 caracteres
- Login mínimo de 3 caracteres
- Senha mínimo de 6 caracteres

---

### Veiculo

Campos obrigatórios:

- Id
- Descricao
- Marca
- Modelo

Campos opcionais:

- Opcionais
- Valor

Regras:

- Descricao obrigatória e máximo 100 caracteres
- Marca obrigatória
- Modelo obrigatório e máximo 30 caracteres

---

## Fluxo de autenticação

1. Usuário realiza cadastro.
2. Usuário realiza login informando login e senha.
3. A aplicação gera um token JWT.
4. O token deve ser enviado no header das requisições protegidas.

Header esperado:
Authorization: Bearer {token}

---

## Padrão de status codes esperado

- 200 OK -> operação bem sucedida
- 201 Created -> recurso criado
- 204 No Content -> operação concluída sem retorno

Erros:

- 400 BadRequest -> erro de validação
- 401 Unauthorized -> token inválido ou login inválido
- 403 Forbidden -> acesso sem permissão
- 404 NotFound -> recurso não encontrado

---

## Premissas assumidas

- Controllers devem apenas receber requisições HTTP
- Toda regra de negócio deve estar fora dos controllers
- Comunicação entre controllers e aplicação deve ocorrer via MediatR
- Persistência será implementada via EF Core InMemory

