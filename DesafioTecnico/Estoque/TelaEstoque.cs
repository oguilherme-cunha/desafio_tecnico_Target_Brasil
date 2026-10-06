using DesafioTecnico.Comum;

namespace DesafioTecnico.Estoque;

public class TelaEstoque : ITela
{
    private readonly ServicoEstoque _servico;

    public TelaEstoque(ServicoEstoque servico)
    {
        _servico = servico;
    }

    public string Titulo => "Movimentação de estoque";

    public void Executar()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Estoque ---");
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Dar entrada");
            Console.WriteLine("3 - Dar saída");
            Console.WriteLine("4 - Histórico de movimentações");
            Console.WriteLine("0 - Voltar");

            int opcao = LeitorConsole.LerInteiro("Opção: ");

            if (opcao == 0) return;
            else if (opcao == 1) ListarProdutos();
            else if (opcao == 2) Movimentar(entrada: true);
            else if (opcao == 3) Movimentar(entrada: false);
            else if (opcao == 4) ListarHistorico();
            else Console.WriteLine("Opção inválida.");
        }
    }

    private void ListarProdutos()
    {
        Console.WriteLine($"{"Código",-8}{"Descrição",-30}{"Estoque",8}");
        foreach (Produto p in _servico.ObterProdutos())
        {
            Console.WriteLine($"{p.Codigo,-8}{p.Descricao,-30}{p.Estoque,8}");
        }
    }

    private void Movimentar(bool entrada)
    {
        int codigo = LeitorConsole.LerInteiro("Código do produto: ");
        int quantidade = LeitorConsole.LerInteiro("Quantidade: ");
        string descricao = LeitorConsole.LerTexto("Descrição (ex.: Compra de fornecedor, Venda, Devolução): ");

        try
        {
            Movimentacao mov;
            if (entrada)
            {
                mov = _servico.DarEntrada(codigo, quantidade, descricao);
            }
            else
            {
                mov = _servico.DarSaida(codigo, quantidade, descricao);
            }

            Console.WriteLine($"Movimentação #{mov.Id} registrada. Estoque final do produto {mov.CodigoProduto}: {mov.EstoqueFinal}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private void ListarHistorico()
    {
        List<Movimentacao> historico = _servico.ObterHistorico();
        if (historico.Count == 0)
        {
            Console.WriteLine("Nenhuma movimentação lançada.");
            return;
        }

        foreach (Movimentacao m in historico)
        {
            Console.WriteLine($"#{m.Id} {m.Data:dd/MM/yyyy HH:mm} | Produto {m.CodigoProduto} | {m.Tipo} de {m.Quantidade} | {m.Descricao} | Saldo: {m.EstoqueFinal}");
        }
    }
}
