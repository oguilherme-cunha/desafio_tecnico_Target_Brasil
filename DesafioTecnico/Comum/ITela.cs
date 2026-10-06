namespace DesafioTecnico.Comum;

/// <summary>Toda tela do menu segue este contrato.</summary>
public interface ITela
{
    string Titulo { get; }
    void Executar();
}
