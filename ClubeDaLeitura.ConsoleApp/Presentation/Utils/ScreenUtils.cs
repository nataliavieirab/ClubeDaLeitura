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

    ShowEnterMessage();
  }

  public void ShowEnterMessage()
  {
    Console.WriteLine("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowError(string[] errors)
  {
    Console.WriteLine();

    for (int i = 0; i < errors.Length; i++)
    {
      string error = errors[i];

      ShowUISimpleLine();

      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(error);
      Console.ResetColor();

      ShowUISimpleLine();
    }

    ShowEnterMessage();
  }

  public string GetEntityID(string entityName)
  {
    string? selectedId;

    do
    {
      Console.WriteLine($"\nDigite o ID do/a {entityName}");
      Console.Write("> ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;
    } while (true);

    return selectedId;
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