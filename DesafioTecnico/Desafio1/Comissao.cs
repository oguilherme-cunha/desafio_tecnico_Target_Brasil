using System.Text.Json;

namespace DesafioTecnico.Desafio1;

public sealed class Venda
{
    public string Vendedor { get; init; } = string.Empty;
    public decimal Valor { get; init; }
}

public sealed class ArquivoVendas
{
    public List<Venda> Vendas { get; init; } = new();
}

public sealed record ResumoComissao(
    string Vendedor,
    int QuantidadeVendas,
    decimal TotalVendido,
    decimal TotalComissao);

public static class CalculadoraComissao
{
    private const decimal LimiteSemComissao = 100m;
    private const decimal LimiteFaixaMaior = 500m;
    private const decimal PercentualFaixaMenor = 0.01m;
    private const decimal PercentualFaixaMaior = 0.05m;

    /// <summary>Calcula a comissão de UMA venda, conforme a faixa do valor.</summary>
    public static decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 0)
            throw new ArgumentOutOfRangeException(nameof(valorVenda), "Valor de venda não pode ser negativo.");

        return valorVenda switch
        {
            < LimiteSemComissao => 0m,                               // abaixo de R$ 100
            < LimiteFaixaMaior  => valorVenda * PercentualFaixaMenor, // de R$ 100 até R$ 499,99
            _                   => valorVenda * PercentualFaixaMaior  // a partir de R$ 500
        };
    }

    /// <summary>Agrupa as vendas por vendedor e soma as comissões.</summary>
    public static List<ResumoComissao> CalcularPorVendedor(IEnumerable<Venda> vendas) =>
        vendas
            .GroupBy(v => v.Vendedor)
            .Select(g => new ResumoComissao(
                g.Key,
                g.Count(),
                g.Sum(v => v.Valor),
                Math.Round(g.Sum(v => CalcularComissao(v.Valor)), 2, MidpointRounding.AwayFromZero)))
            .OrderByDescending(r => r.TotalComissao)
            .ToList();
}

public static class ComissaoApp
{
    public static void Executar()
    {
        var caminho = Path.Combine(AppContext.BaseDirectory, "Data", "vendas.json");
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var arquivo = JsonSerializer.Deserialize<ArquivoVendas>(File.ReadAllText(caminho), opcoes)
                      ?? throw new InvalidOperationException("Arquivo de vendas inválido.");

        var resumos = CalculadoraComissao.CalcularPorVendedor(arquivo.Vendas);

        Console.WriteLine();
        Console.WriteLine($"{"Vendedor",-20}{"Vendas",8}{"Total vendido",18}{"Comissão",14}");
        Console.WriteLine(new string('-', 60));
        foreach (var r in resumos)
            Console.WriteLine($"{r.Vendedor,-20}{r.QuantidadeVendas,8}{r.TotalVendido,18:C}{r.TotalComissao,14:C}");
    }
}
