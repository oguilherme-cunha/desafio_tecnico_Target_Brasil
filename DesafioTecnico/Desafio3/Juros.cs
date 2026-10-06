using System.Globalization;

namespace DesafioTecnico.Desafio3;

public sealed record ResultadoJuros(int DiasAtraso, decimal Juros, decimal ValorAtualizado);

public static class CalculadoraJuros
{
    public const decimal TaxaDiaria = 0.025m; // 2,5% ao dia

    /// <summary>
    /// Juros simples: valor x 2,5% x dias de atraso.
    /// A data de referência é parâmetro (e não DateTime.Today direto) para o cálculo ser testável.
    /// </summary>
    public static ResultadoJuros Calcular(decimal valor, DateOnly vencimento, DateOnly dataReferencia)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

        var diasAtraso = Math.Max(0, dataReferencia.DayNumber - vencimento.DayNumber);
        var juros = Math.Round(valor * TaxaDiaria * diasAtraso, 2, MidpointRounding.AwayFromZero);

        return new ResultadoJuros(diasAtraso, juros, valor + juros);
    }
}

public static class JurosApp
{
    public static void Executar()
    {
        var valor = LerDecimal("Valor do título (ex.: 1500,00): ");
        var vencimento = LerData("Data de vencimento (dd/MM/aaaa): ");
        var hoje = DateOnly.FromDateTime(DateTime.Today);

        var r = CalculadoraJuros.Calcular(valor, vencimento, hoje);

        Console.WriteLine();
        Console.WriteLine($"Data de hoje:     {hoje:dd/MM/yyyy}");
        Console.WriteLine($"Dias de atraso:   {r.DiasAtraso}");
        Console.WriteLine($"Juros (2,5%/dia): {r.Juros:C}");
        Console.WriteLine($"Valor atualizado: {r.ValorAtualizado:C}");
    }

    private static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.CurrentCulture, out var valor) && valor > 0)
                return valor;
            Console.WriteLine("Valor inválido.");
        }
    }

    private static DateOnly LerData(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (DateOnly.TryParseExact(Console.ReadLine()?.Trim(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
                return data;
            Console.WriteLine("Data inválida. Use o formato dd/MM/aaaa.");
        }
    }
}
