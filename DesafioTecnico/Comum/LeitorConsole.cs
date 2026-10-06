using System.Globalization;

namespace DesafioTecnico.Comum;

/// <summary>Responsável apenas por ler e validar o que o usuário digita.</summary>
public static class LeitorConsole
{
    public static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine() ?? "";
    }

    public static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                return valor;
            }
            Console.WriteLine("Valor inválido, digite um número inteiro.");
        }
    }

    public static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (decimal.TryParse(Console.ReadLine(), out decimal valor))
            {
                return valor;
            }
            Console.WriteLine("Valor inválido.");
        }
    }

    public static DateTime LerData(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
            {
                return data;
            }
            Console.WriteLine("Data inválida. Use o formato dd/MM/aaaa.");
        }
    }
}
