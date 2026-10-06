using System.Text.Json;

namespace DesafioTecnico.Estoque;

/// <summary>Carrega os produtos do JSON e os mantém em memória.</summary>
public class ProdutoRepositoryJson : IProdutoRepository
{
    private readonly List<Produto> _produtos = new();

    public ProdutoRepositoryJson(string caminhoArquivo)
    {
        string conteudo = File.ReadAllText(caminhoArquivo);
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        ArquivoEstoqueJson? arquivo = JsonSerializer.Deserialize<ArquivoEstoqueJson>(conteudo, opcoes);

        if (arquivo == null)
        {
            return;
        }

        foreach (ProdutoJson item in arquivo.Estoque)
        {
            _produtos.Add(new Produto(item.CodigoProduto, item.DescricaoProduto, item.Estoque));
        }
    }

    public List<Produto> ObterTodos()
    {
        return _produtos;
    }

    public Produto? ObterPorCodigo(int codigo)
    {
        foreach (Produto produto in _produtos)
        {
            if (produto.Codigo == codigo)
            {
                return produto;
            }
        }
        return null;
    }

    // Classes que espelham o formato do JSON
    private class ArquivoEstoqueJson
    {
        public List<ProdutoJson> Estoque { get; set; } = new();
    }

    private class ProdutoJson
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; } = "";
        public int Estoque { get; set; }
    }
}
