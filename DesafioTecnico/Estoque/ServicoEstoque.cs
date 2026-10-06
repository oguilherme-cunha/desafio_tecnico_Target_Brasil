namespace DesafioTecnico.Estoque;

/// <summary>Registra as movimentações e guarda o histórico.</summary>
public class ServicoEstoque
{
    private readonly IProdutoRepository _produtos;
    private readonly List<Movimentacao> _historico = new();
    private int _ultimoId = 0;

    public ServicoEstoque(IProdutoRepository produtos)
    {
        _produtos = produtos;
    }

    public List<Produto> ObterProdutos()
    {
        return _produtos.ObterTodos();
    }

    public List<Movimentacao> ObterHistorico()
    {
        return _historico;
    }

    public Movimentacao DarEntrada(int codigoProduto, int quantidade, string descricao)
    {
        var movimentacao = new MovimentacaoEntrada(_ultimoId + 1, codigoProduto, quantidade, descricao);
        return Registrar(movimentacao);
    }

    public Movimentacao DarSaida(int codigoProduto, int quantidade, string descricao)
    {
        var movimentacao = new MovimentacaoSaida(_ultimoId + 1, codigoProduto, quantidade, descricao);
        return Registrar(movimentacao);
    }

    private Movimentacao Registrar(Movimentacao movimentacao)
    {
        Produto? produto = _produtos.ObterPorCodigo(movimentacao.CodigoProduto);
        if (produto == null)
        {
            throw new ArgumentException($"Produto {movimentacao.CodigoProduto} não encontrado.");
        }

        movimentacao.Executar(produto);   // se der erro aqui, nada é registrado

        _historico.Add(movimentacao);
        _ultimoId = movimentacao.Id;
        return movimentacao;
    }
}
