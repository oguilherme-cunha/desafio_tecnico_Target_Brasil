namespace DesafioTecnico.Comissao;

/// <summary>Agrupa as vendas por vendedor e soma a comissão de cada um.</summary>
public class CalculadoraComissao
{
    private readonly IRegraComissao _regra;

    public CalculadoraComissao(IRegraComissao regra)
    {
        _regra = regra;
    }

    public List<ComissaoVendedor> CalcularPorVendedor(List<Venda> vendas)
    {
        var resultado = new List<ComissaoVendedor>();
        var vendedores = new Dictionary<string, ComissaoVendedor>();

        foreach (Venda venda in vendas)
        {
            if (!vendedores.ContainsKey(venda.Vendedor))
            {
                var novo = new ComissaoVendedor(venda.Vendedor);
                vendedores.Add(venda.Vendedor, novo);
                resultado.Add(novo);
            }

            decimal comissao = _regra.Calcular(venda.Valor);
            vendedores[venda.Vendedor].AdicionarVenda(venda.Valor, comissao);
        }

        return resultado;
    }
}
