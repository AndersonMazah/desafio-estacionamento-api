# Este documento contém minhas anotações referentes a SITUAÇÕES e DECISÕES enfrentadas.

---

**SITUAÇÃO 1**: O enunciado informa que a API deve utilizar autenticação JWT. Porém, surge um problema lógico: Se todos os endpoints exigirem token, como será realizado o cadastro inicial de usuários e o login?
**DECISÃO 1**: Manter dois endpoints públicos, o Cadastro de usuário e Login. Todos os demais endpoints permanecem protegidos por JWT.
