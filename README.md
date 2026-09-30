# Advanced Business Development with .NET
## Aula 01 de 04: Persistência NoSQL e Padrão Repository com MongoDB Atlas

Este repositório contém o código-fonte desenvolvido na Aula 01 da série de 4 aulas práticas da disciplina de Advanced Business Development with .NET. O objetivo principal desta primeira etapa é substituir a persistência relacional por um banco de dados NoSQL gerenciado em nuvem (MongoDB Atlas), implementando o Padrão Repository e mantendo as boas práticas de Clean Architecture e SOLID.

---

### 📌 Tópicos Abordados na Aula 01
- Conexão NoSQL: Integração do ASP.NET Core com o MongoDB Atlas via driver oficial MongoDB.Driver.
- Padrão Repository: Abstração do acesso a dados desacoplando o banco de dados da regra de negócio.
- Mapeamento BSON: Mapeamento de atributos e chave primária GUID para documentos NoSQL.
- Injeção de Dependência: Registro das configurações e serviços no contêiner nativo do .NET.
- Swagger / OpenAPI: Interface interativa para teste e validação dos endpoints.

---

### 📂 Estrutura do Projeto
- AulasCSharp/
  - src/AulasCSharp/
    - Properties/launchSettings.json
    - Configs/MongoDbSettings.cs
    - Dominio/Entidades/Produto.cs
    - Dominio/Interfaces/IProdutoRepositorio.cs
    - Infraestrutura/Repositorios/MongoProdutoRepositorio.cs
    - Aplicacao/Dtos/CriarProdutoRequest.cs
    - Aplicacao/Servicos/IProdutoServico.cs
    - Aplicacao/Servicos/ProdutoServico.cs
    - appsettings.json
    - Program.cs
  - AulasCSharp.sln

---

### 🚀 Como Executar o Projeto
1. Clone o repositório: git clone https://github.com/profalexresende/csharp_mongoDB_2026.git
2. Acesse a pasta da aplicação: cd csharp_mongoDB_2026/src/AulasCSharp
3. Configure a sua Connection String do MongoDB Atlas no arquivo appsettings.json no campo MongoDbSettings:ConnectionString.
4. Execute o projeto: dotnet run
5. Acesse a interface gráfica do Swagger para testar as rotas no endereço: https://localhost:7290/swagger

---

### 🗺️ Próximas Aulas do Módulo (1 de 4)
- Aula 01 [Atual]: Persistência NoSQL com MongoDB Atlas e Padrão Repository.
- Aula 02: Paginação, Ordenação e Filtros Avançados em APIs RESTful.
- Aula 03: HATEOAS e Navegabilidade no Nível 3 da Maturidade RESTful.
- Aula 04: Segurança, Autenticação e Autorização com JWT e Swagger.

---

Prof. Alex Sander
