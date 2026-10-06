namespace DesafioTecnico.Estoque;

public class MovimentacaoSaida : Movimentacao
{
    public MovimentacaoSaida(int id, int codigoProduto, int quantidade, string descricao)
        : base(id, codigoProduto, quantidade, descricao)
    {
    }

    public override string Tipo => "Saída";

    protected override void Aplicar(Produto produto)
    {
        produto.RemoverEstoque(Quantidade);
    }
}
