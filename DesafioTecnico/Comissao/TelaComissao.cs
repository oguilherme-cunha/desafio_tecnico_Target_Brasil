using DesafioTecnico.Comum;

namespace DesafioTecnico.Comissao;

public class TelaComissao : ITela
{
    private readonly IVendaRepository _repositorio;
    private readonly CalculadoraComissao _calculadora;

    public TelaComissao(IVendaRepository repositorio, CalculadoraComissao calculadora)
    {
        _repositorio = repositorio;
        _calculadora = calculadora;
    }

    public string Titulo => "Comissão de vendedores";

    public void Executar()
    {
        List<Venda> vendas = _repositorio.ObterTodas();
        List<ComissaoVendedor> comissoes = _calculadora.CalcularPorVendedor(vendas);

        Console.WriteLine();
        Console.WriteLine($"{"Vendedor",-20}{"Vendas",8}{"Total vendido",18}{"Comissão",14}");
        Console.WriteLine(new string('-', 60));

        foreach (ComissaoVendedor c in comissoes)
        {
            Console.WriteLine($"{c.Vendedor,-20}{c.QuantidadeVendas,8}{c.TotalVendido,18:C}{c.TotalComissao,14:C}");
        }
    }
}
