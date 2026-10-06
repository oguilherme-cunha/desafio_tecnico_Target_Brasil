namespace DesafioTecnico.Estoque;

public class MovimentacaoEntrada : Movimentacao
{
    public MovimentacaoEntrada(int id, int codigoProduto, int quantidade, string descricao)
        : base(id, codigoProduto, quantidade, descricao)
    {
    }

    public override string Tipo => "Entrada";

    protected override void Aplicar(Produto produto)
    {
        produto.AdicionarEstoque(Quantidade);
    }
}
