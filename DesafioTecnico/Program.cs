using System.Globalization;
using System.Text;
using DesafioTecnico.Comissao;
using DesafioTecnico.Comum;
using DesafioTecnico.Estoque;
using DesafioTecnico.Juros;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

// Aqui as peças são montadas: cada classe recebe pronto aquilo de que depende.
string pastaDados = Path.Combine(AppContext.BaseDirectory, "Data");

var telaComissao = new TelaComissao(
    new VendaRepositoryJson(Path.Combine(pastaDados, "vendas.json")),
    new CalculadoraComissao(new RegraComissaoPorFaixa()));

var telaEstoque = new TelaEstoque(
    new ServicoEstoque(new ProdutoRepositoryJson(Path.Combine(pastaDados, "estoque.json"))));

var telaJuros = new TelaJuros(new CalculadoraJurosSimples(0.025m));

var telas = new List<ITela> { telaComissao, telaEstoque, telaJuros };

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== DESAFIO TÉCNICO ===");
    for (int i = 0; i < telas.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {telas[i].Titulo}");
    }
    Console.WriteLine("0 - Sair");

    int opcao = LeitorConsole.LerInteiro("Opção: ");

    if (opcao == 0)
    {
        break;
    }

    if (opcao < 1 || opcao > telas.Count)
    {
        Console.WriteLine("Opção inválida.");
        continue;
    }

    telas[opcao - 1].Executar();
}
