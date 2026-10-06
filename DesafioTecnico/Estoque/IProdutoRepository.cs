namespace DesafioTecnico.Estoque;

public interface IProdutoRepository
{
    List<Produto> ObterTodos();
    Produto? ObterPorCodigo(int codigo);
}
