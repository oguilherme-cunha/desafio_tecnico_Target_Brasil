namespace DesafioTecnico.Estoque;

/// <summary>
/// Base de toda movimentação. Cada tipo (entrada, saída...) diz
/// como altera o produto implementando o método Aplicar.
/// </summary>
public abstract class Movimentacao
{
    public int Id { get; }
    public int CodigoProduto { get; }
    public int Quantidade { get; }
    public string Descricao { get; }
    public DateTime Data { get; }
    public int EstoqueFinal { get; private set; }

    public abstract string Tipo { get; }

    protected Movimentacao(int id, int codigoProduto, int quantidade, string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("Informe uma descrição para a movimentação.");
        }

        Id = id;
        CodigoProduto = codigoProduto;
        Quantidade = quantidade;
        Descricao = descricao.Trim();
        Data = DateTime.Now;
    }

    public void Executar(Produto produto)
    {
        Aplicar(produto);
        EstoqueFinal = produto.Estoque;
    }

    protected abstract void Aplicar(Produto produto);
}
