namespace DesafioTecnico.Comissao;

/// <summary>
/// Regra do desafio:
/// abaixo de R$ 100 = sem comissão; abaixo de R$ 500 = 1%; a partir de R$ 500 = 5%.
/// </summary>
public class RegraComissaoPorFaixa : IRegraComissao
{
    public decimal Calcular(decimal valorVenda)
    {
        if (valorVenda < 100)
        {
            return 0;
        }

        if (valorVenda < 500)
        {
            return valorVenda * 0.01m;
        }

        return valorVenda * 0.05m;
    }
}
