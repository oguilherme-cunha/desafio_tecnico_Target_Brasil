namespace DesafioTecnico.Juros;

/// <summary>Juros simples: valor x taxa diária x dias de atraso.</summary>
public class CalculadoraJurosSimples : ICalculadoraJuros
{
    private readonly decimal _taxaDiaria;

    public CalculadoraJurosSimples(decimal taxaDiaria)
    {
        _taxaDiaria = taxaDiaria;
    }

    public decimal Calcular(Titulo titulo, DateTime dataReferencia)
    {
        int dias = titulo.DiasEmAtraso(dataReferencia);
        decimal juros = titulo.Valor * _taxaDiaria * dias;
        return Math.Round(juros, 2);
    }
}
