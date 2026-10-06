using DesafioTecnico.Comum;

namespace DesafioTecnico.Juros;

public class TelaJuros : ITela
{
    private readonly ICalculadoraJuros _calculadora;

    public TelaJuros(ICalculadoraJuros calculadora)
    {
        _calculadora = calculadora;
    }

    public string Titulo => "Cálculo de juros por atraso";

    public void Executar()
    {
        decimal valor = LeitorConsole.LerDecimal("Valor do título (ex.: 1500,00): ");
        DateTime vencimento = LeitorConsole.LerData("Data de vencimento (dd/MM/aaaa): ");
        DateTime hoje = DateTime.Today;

        try
        {
            var titulo = new Titulo(valor, vencimento);
            decimal juros = _calculadora.Calcular(titulo, hoje);

            Console.WriteLine();
            Console.WriteLine($"Data de hoje:     {hoje:dd/MM/yyyy}");
            Console.WriteLine($"Dias de atraso:   {titulo.DiasEmAtraso(hoje)}");
            Console.WriteLine($"Juros:            {juros:C}");
            Console.WriteLine($"Valor atualizado: {titulo.Valor + juros:C}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
