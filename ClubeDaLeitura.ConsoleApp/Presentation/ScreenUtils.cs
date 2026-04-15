namespace ClubeDaLeitura.ConsoleApp.Presentation;

static class ScreenUtils
{
  public static string GetMainMenuOption()
  {
    ShowMainHeader("Clube da Leitura");
    Console.WriteLine("\n1 - Gerenciar Caixas de Revistas");
    Console.WriteLine("2 - Gerenciar Revistas");
    Console.WriteLine("3 - Gerenciar Amigos");
    Console.WriteLine("4 - Gerenciar Empréstimos");
    Console.WriteLine("S - Sair");
    Console.Write("\n> ");
    return Console.ReadLine()?.ToUpper()!;
  }

  public static void ShowMainHeader(string title)
  {
    string line = GetUIDoubleLine();

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine($"--------------------------------------- {title} ------------------------------------------");
    Console.WriteLine(line);
  }

  public static void ShowOperationHeader(string operation)
  {
    string centeredText = new string(' ', 38) + operation;

    Console.WriteLine($"\n{centeredText}");
  }

  public static string GetUIDoubleLine()
  {
    return "=========================================================================================================";
  }

  public static void ShowUISimpleLine()
  {
    Console.WriteLine("---------------------------------------------------------------------------------------------------------");
  }
}