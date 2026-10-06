namespace DesafioTecnico.Estoque;

/// <summary>
/// O estoque só muda pelos métodos da própria classe,
/// então ninguém consegue deixá-lo negativo "por fora".
/// </summary>
public class Produto
{
    public int Codigo { get; }
    public string Descricao { get; }
    public int Estoque { get; private set; }

    public Produto(int codigo, string descricao, int estoque)
    {
        Codigo = codigo;
        Descricao = descricao;
        Estoque = estoque;
    }

    public void AdicionarEstoque(int quantidade)
    {
        ValidarQuantidade(quantidade);
        Estoque += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        ValidarQuantidade(quantidade);

        if (quantidade > Estoque)
        {
            throw new InvalidOperationException($"Estoque insuficiente. Disponível: {Estoque}.");
        }

        Estoque -= quantidade;
    }

    private static void ValidarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        }
    }
}
