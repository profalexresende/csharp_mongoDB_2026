using AulasCSharp.Dominio;
using AulasCSharp.Infraestrutura;

// Cria o construtor da aplicação Web ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Registra o serviço para explorar as rotas da API na documentação
builder.Services.AddEndpointsApiExplorer();
// Registra o gerador da interface visual do Swagger
builder.Services.AddSwaggerGen();

// Registra a Injeção de Dependência do Repositório no container nativo do .NET
// AddScoped garante que uma nova instância do repositório seja criada por requisição HTTP
builder.Services.AddScoped<IProdutoRepositorio, MongoProdutoRepositorio>();

// Constrói a aplicação com os serviços configurados
var app = builder.Build();

// Configura o middleware de execução no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    // Ativa a geração do arquivo JSON da especificação OpenAPI
    app.UseSwagger();
    // Ativa a interface visual e interativa do Swagger UI no navegador
    app.UseSwaggerUI();
}

// Redireciona automaticamente requisições HTTP para HTTPS
app.UseHttpsRedirection();

// ----------------------------------------------------------------------------------
// ENDPOINTS DA MINIMAL API
// ----------------------------------------------------------------------------------

// Rota GET: /api/produtos
// O .NET injeta automaticamente o IProdutoRepositorio cadastrado na DI no parâmetro "repo"
app.MapGet("/api/produtos", async (IProdutoRepositorio repo) =>
    Results.Ok(await repo.ObterTodosAsync()));

// Rota GET: /api/produtos/{id} (Restrita a tipos GUID válidos)
app.MapGet("/api/produtos/{id:guid}", async (Guid id, IProdutoRepositorio repo) =>
{
    // Executa a busca assíncrona pelo identificador
    var produto = await repo.ObterPorIdAsync(id);

    // Retorna HTTP Status 200 (OK) se o produto existir, ou HTTP Status 404 (Not Found)
    return produto is not null
        ? Results.Ok(produto)
        : Results.NotFound(new { mensagem = "Produto não encontrado." });
});

// Rota POST: /api/produtos
app.MapPost("/api/produtos", async (CriarProdutoRequest request, IProdutoRepositorio repo) =>
{
    try
    {
        // Instancia o modelo de domínio (que dispara as validações internas da classe Produto)
        var produto = new Produto(request.Nome, request.Preco, request.Estoque);

        // Persiste o objeto validado no banco de dados MongoDB
        await repo.AdicionarAsync(produto);

        // Retorna HTTP Status 201 (Created) acompanhado da URL de acesso ao recurso e do objeto
        return Results.Created($"/api/produtos/{produto.Id}", produto);
    }
    catch (ArgumentException ex)
    {
        // Captura exceções das regras de negócio e retorna HTTP Status 400 (Bad Request) com a mensagem tratada
        return Results.BadRequest(new { erro = ex.Message });
    }
});

// Executa a aplicação e inicia a escuta das requisições HTTP
app.Run();


// DECLARAÇÃO DE TIPOS / DTOs

// Record imutável utilizado como Data Transfer Object (DTO) para deserializar o JSON recebido na requisição POST
record CriarProdutoRequest(string Nome, decimal Preco, int Estoque);