namespace DesafioTecnico.Juros;

public class Titulo
{
    public decimal Valor { get; }
    public DateTime Vencimento { get; }

    public Titulo(decimal valor, DateTime vencimento)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero.");
        }

        Valor = valor;
        Vencimento = vencimento.Date;
    }

    public int DiasEmAtraso(DateTime dataReferencia)
    {
        int dias = (dataReferencia.Date - Vencimento).Days;
        if (dias < 0)
        {
            return 0;   // ainda não venceu
        }
        return dias;
    }
}
