using System.Text.Json;

namespace DesafioTecnico.Comissao;

/// <summary>Lê as vendas do arquivo JSON.</summary>
public class VendaRepositoryJson : IVendaRepository
{
    private readonly string _caminhoArquivo;

    public VendaRepositoryJson(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
    }

    public List<Venda> ObterTodas()
    {
        string conteudo = File.ReadAllText(_caminhoArquivo);
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        ArquivoVendasJson? arquivo = JsonSerializer.Deserialize<ArquivoVendasJson>(conteudo, opcoes);

        var vendas = new List<Venda>();
        if (arquivo == null)
        {
            return vendas;
        }

        foreach (VendaJson item in arquivo.Vendas)
        {
            vendas.Add(new Venda(item.Vendedor, item.Valor));
        }

        return vendas;
    }

    // Classes que espelham o formato do JSON
    private class ArquivoVendasJson
    {
        public List<VendaJson> Vendas { get; set; } = new();
    }

    private class VendaJson
    {
        public string Vendedor { get; set; } = "";
        public decimal Valor { get; set; }
    }
}
