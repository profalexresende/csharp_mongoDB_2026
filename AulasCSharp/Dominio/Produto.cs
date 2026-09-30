// Importa o namespace necessário para trabalhar com os tipos nativos do BSON/MongoDB
using MongoDB.Bson;
// Importa anotações (atributos) para configurar o mapeamento da classe para o banco
using MongoDB.Bson.Serialization.Attributes;

namespace AulasCSharp.Dominio;

// Entidade principal do domínio
public class Produto
{
    // Define esta propriedade como o identificador único (_id) do documento no MongoDB
    [BsonId]
    // Converte o tipo Guid do C# para uma String legível dentro do banco de dados
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }

    // Propriedade Nome (com 'private set' para impedir alterações externas descontroladas)
    public string Nome { get; private set; } = string.Empty;

    // Mapeia a propriedade decimal para o tipo Decimal128 do MongoDB, garantindo precisão financeira
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Preco { get; private set; }

    // Quantidade física em estoque
    public int Estoque { get; private set; }

    // Construtor público que exige os dados necessários e garante a criação de instâncias válidas
    public Produto(string nome, decimal preco, int estoque)
    {
        // Validação 1: O nome do produto não pode ser nulo, vazio ou conter apenas espaços
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        // Validação 2: O preço deve ser estritamente maior que zero
        if (preco <= 0)
            throw new ArgumentException("O preço do produto deve ser maior que zero.");

        // Validação 3: O estoque não pode ser um valor negativo
        if (estoque < 0)
            throw new ArgumentException("O estoque não pode ser negativo.");

        // Gera um novo GUID (identificador único universal) para a nova entidade
        Id = Guid.NewGuid();
        // Atribui os parâmetros validados às propriedades internas
        Nome = nome;
        Preco = preco;
        Estoque = estoque;
    }
}