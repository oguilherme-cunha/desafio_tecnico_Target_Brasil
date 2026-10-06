namespace DesafioTecnico.Comissao;

/// <summary>Acumula as vendas e a comissão de um vendedor.</summary>
public class ComissaoVendedor
{
    public string Vendedor { get; }
    public int QuantidadeVendas { get; private set; }
    public decimal TotalVendido { get; private set; }
    public decimal TotalComissao { get; private set; }

    public ComissaoVendedor(string vendedor)
    {
        Vendedor = vendedor;
    }

    public void AdicionarVenda(decimal valorVenda, decimal comissao)
    {
        QuantidadeVendas++;
        TotalVendido += valorVenda;
        TotalComissao += comissao;
    }
}
