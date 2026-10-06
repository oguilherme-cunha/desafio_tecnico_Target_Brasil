namespace DesafioTecnico.Comissao;

public class Venda
{
    public string Vendedor { get; }
    public decimal Valor { get; }

    public Venda(string vendedor, decimal valor)
    {
        Vendedor = vendedor;
        Valor = valor;
    }
}
