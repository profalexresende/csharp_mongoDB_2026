namespace AulasCSharp.Dominio;

// Interface do Padrão Repository
// Define o contrato de persistência da aplicação sem acoplar o código a um banco específico
public interface IProdutoRepositorio
{
    // Contrato para buscar e retornar todos os produtos salvos de forma assíncrona
    Task<IEnumerable<Produto>> ObterTodosAsync();

    // Contrato para buscar um produto pelo seu GUID (pode retornar nulo se não encontrar)
    Task<Produto?> ObterPorIdAsync(Guid id);

    // Contrato para gravar um novo objeto Produto no banco de dados
    Task AdicionarAsync(Produto produto);
}