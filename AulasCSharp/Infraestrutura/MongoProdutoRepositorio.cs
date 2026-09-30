using AulasCSharp.Dominio;
// Importa a biblioteca oficial do driver do MongoDB para .NET
using MongoDB.Driver;

namespace AulasCSharp.Infraestrutura;

// Implementação concreta do repositório utilizando o driver do MongoDB Atlas
public class MongoProdutoRepositorio : IProdutoRepositorio
{
    // Guarda a referência da coleção "Produtos" fortemente tipada no MongoDB
    private readonly IMongoCollection<Produto> _collection;

    // O .NET injeta automaticamente a interface IConfiguration para ler o appsettings.json
    public MongoProdutoRepositorio(IConfiguration config)
    {
        // Lê o valor da chave "ConnectionString" dentro de "MongoDbSettings" no JSON
        var connectionString = config["MongoDbSettings:ConnectionString"];
        // Lê o nome da base de dados definida na chave "DatabaseName" no JSON
        var databaseName = config["MongoDbSettings:DatabaseName"];

        // Cria o cliente de rede do MongoDB com as credenciais obtidas
        var client = new MongoClient(connectionString);
        // Conecta ou obtém a base de dados no cluster Atlas
        var database = client.GetDatabase(databaseName);
        // Obtém a coleção de documentos "Produtos"
        _collection = database.GetCollection<Produto>("Produtos");
    }

    // Consulta todos os documentos da coleção (_ => true funciona como um 'SELECT *')
    public async Task<IEnumerable<Produto>> ObterTodosAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    // Filtra e retorna o primeiro documento onde o Id seja igual ao Guid informado
    public async Task<Produto?> ObterPorIdAsync(Guid id) =>
        await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

    // Escreve um novo documento na coleção do MongoDB de forma assíncrona
    public async Task AdicionarAsync(Produto produto) =>
        await _collection.InsertOneAsync(produto);
}