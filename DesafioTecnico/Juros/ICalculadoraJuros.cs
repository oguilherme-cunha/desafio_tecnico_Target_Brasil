namespace DesafioTecnico.Juros;

public interface ICalculadoraJuros
{
    decimal Calcular(Titulo titulo, DateTime dataReferencia);
}
