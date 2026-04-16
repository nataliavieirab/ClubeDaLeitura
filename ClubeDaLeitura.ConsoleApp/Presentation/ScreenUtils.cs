namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class ScreenUtils
{

  public string? title;

  public ScreenUtils(string _title)
  {
    title = _title;
  }

  public string GetMainMenuOption()
  {
    MainHeader();
    Console.WriteLine("\n[1] Gerenciar Caixas de Revistas");
    Console.WriteLine("[2] Gerenciar Revistas");
    Console.WriteLine("[3] Gerenciar Amigos");
    Console.WriteLine("[4] Gerenciar Empréstimos");
    Console.WriteLine("[S] Sair");
    Console.Write("\n> ");
    return Console.ReadLine()?.ToUpper()!;
  }

  public void MainHeader()
  {
    string line = GetUIDoubleLine();

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine($"------------------------------------------ {title} ----------------------------------------------");
    Console.WriteLine(line);
  }

  public void OperationHeader(string operation)
  {
    MainHeader();

    string centeredText = new string(' ', 42) + operation;
    Console.WriteLine($"\n{centeredText.ToUpper()}");
  }

  public void ShowMessage(string message)
  {
    Console.WriteLine();

    ShowUISimpleLine();
    Console.WriteLine(message);
    ShowUISimpleLine();

    Console.WriteLine("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public string GetUIDoubleLine()
  {
    return "==========================================================================================================";
  }

  public void ShowUISimpleLine()
  {
    Console.WriteLine("----------------------------------------------------------------------------------------------------------");
  }
}