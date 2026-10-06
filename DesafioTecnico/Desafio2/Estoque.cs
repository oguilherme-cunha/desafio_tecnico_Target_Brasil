using System.Text.Json;

namespace DesafioTecnico.Desafio2;

public sealed class Produto
{
    public int CodigoProduto { get; init; }
    public string DescricaoProduto { get; init; } = string.Empty;
    public int Estoque { get; set; }
}

public sealed class ArquivoEstoque
{
    public List<Produto> Estoque { get; init; } = new();
}

public enum TipoMovimentacao
{
    Entrada = 1,
    Saida = 2
}

public sealed record Movimentacao(
    int Id,
    int CodigoProduto,
    TipoMovimentacao Tipo,
    int Quantidade,
    string Descricao,
    DateTime DataHora,
    int EstoqueFinal);

public sealed class ServicoEstoque
{
    private readonly Dictionary<int, Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = new();
    private int _ultimoId;

    public ServicoEstoque(IEnumerable<Produto> produtos)
    {
        _produtos = produtos.ToDictionary(p => p.CodigoProduto);
    }

    public IReadOnlyCollection<Produto> Produtos => _produtos.Values;
    public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

    /// <summary>Registra uma entrada ou saída e devolve a movimentação com o estoque final.</summary>
    public Movimentacao Movimentar(int codigoProduto, TipoMovimentacao tipo, int quantidade, string descricao)
    {
        if (!_produtos.TryGetValue(codigoProduto, out var produto))
            throw new ArgumentException($"Produto {codigoProduto} não encontrado.");

        if (!Enum.IsDefined(tipo))
            throw new ArgumentException("Tipo de movimentação inválido.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Informe uma descrição para a movimentação.");

        if (tipo == TipoMovimentacao.Saida && quantidade > produto.Estoque)
            throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}.");

        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        var movimentacao = new Movimentacao(
            ++_ultimoId, codigoProduto, tipo, quantidade, descricao.Trim(), DateTime.Now, produto.Estoque);

        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}

public static class EstoqueApp
{
    public static void Executar()
    {
        var caminho = Path.Combine(AppContext.BaseDirectory, "Data", "estoque.json");
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var arquivo = JsonSerializer.Deserialize<ArquivoEstoque>(File.ReadAllText(caminho), opcoes)
                      ?? throw new InvalidOperationException("Arquivo de estoque inválido.");

        var servico = new ServicoEstoque(arquivo.Estoque);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Estoque ---");
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Lançar movimentação");
            Console.WriteLine("3 - Histórico de movimentações");
            Console.WriteLine("0 - Voltar");
            Console.Write("Opção: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": ListarProdutos(servico); break;
                case "2": LancarMovimentacao(servico); break;
                case "3": ListarHistorico(servico); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    private static void ListarProdutos(ServicoEstoque servico)
    {
        Console.WriteLine($"{"Código",-8}{"Descrição",-30}{"Estoque",8}");
        foreach (var p in servico.Produtos.OrderBy(p => p.CodigoProduto))
            Console.WriteLine($"{p.CodigoProduto,-8}{p.DescricaoProduto,-30}{p.Estoque,8}");
    }

    private static void LancarMovimentacao(ServicoEstoque servico)
    {
        var codigo = LerInteiro("Código do produto: ");

        Console.Write("Tipo (E = entrada, S = saída): ");
        TipoMovimentacao? tipo = Console.ReadLine()?.Trim().ToUpperInvariant() switch
        {
            "E" => TipoMovimentacao.Entrada,
            "S" => TipoMovimentacao.Saida,
            _ => null
        };
        if (tipo is null)
        {
            Console.WriteLine("Tipo inválido.");
            return;
        }

        var quantidade = LerInteiro("Quantidade: ");

        Console.Write("Descrição (ex.: Compra de fornecedor, Venda, Devolução): ");
        var descricao = Console.ReadLine() ?? string.Empty;

        try
        {
            var mov = servico.Movimentar(codigo, tipo.Value, quantidade, descricao);
            Console.WriteLine($"Movimentação #{mov.Id} registrada. Estoque final do produto {mov.CodigoProduto}: {mov.EstoqueFinal}");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private static void ListarHistorico(ServicoEstoque servico)
    {
        if (servico.Movimentacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma movimentação lançada.");
            return;
        }

        foreach (var m in servico.Movimentacoes)
            Console.WriteLine($"#{m.Id} {m.DataHora:dd/MM/yyyy HH:mm} | Produto {m.CodigoProduto} | {m.Tipo} de {m.Quantidade} | {m.Descricao} | Saldo: {m.EstoqueFinal}");
    }

    private static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out var valor))
                return valor;
            Console.WriteLine("Valor inválido, digite um número inteiro.");
        }
    }
}
