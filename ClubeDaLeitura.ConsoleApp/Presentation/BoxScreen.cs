using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class BoxScreen
{

  private readonly ScreenUtils screen = new("Gestão de Caixa");
  private readonly BoxRepository repository;

  public BoxScreen(BoxRepository _repository)
  {

    repository = _repository;
  }

  public string? GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine("\n[1] Cadastrar Caixa");
    Console.WriteLine("[2] Editar Caixa");
    Console.WriteLine("[3] Excluir Caixa");
    Console.WriteLine("[4] Visualizar Caixas");
    Console.WriteLine("[S] Voltar para o início");
    //screen.ShowUISimpleLine();
    Console.Write("\n> ");
    string? mainOption = Console.ReadLine()?.ToUpper();

    return mainOption;
  }

  public void Register()
  {

    screen.OperationHeader("Cadastro de Caixa");

    Box newBox = GetRegistrationData();

    repository?.Create(newBox);

    screen.ShowMessage($"O registro \"{newBox.Id}\" foi cadastrado com sucesso!");
  }

  public void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Caixas");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
    "\n{0, -7} | {1, -20} | {2, -10} | {3, -20}",
    "Id", "Etiqueta", "Cor", "Tempo de Empréstimo"
    );

    Box?[] boxes = [.. repository.FindAll()];

    for (int i = 0; i < boxes.Length; i++)
    {
      Box? box = boxes[i];

      if (box == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
          box.Id, box.Label, box.Color, box.LoanDays
      );
    }

    Console.WriteLine(line);

    if (showHeader)
    {
      Console.Write("\nDigite ENTER para continuar... ");
      Console.ReadLine();
    }
  }

  public void Edit()
  {

    screen.OperationHeader("Edição de Caixa");

    ShowAll(showHeader: false);

    string? selectedId = GetID();

    Console.WriteLine();
    screen.ShowUISimpleLine();

    Box newBox = GetRegistrationData();

    bool success = repository.Update(selectedId, newBox);

    if (!success)
    {
      screen.ShowMessage("Não foi possível encontrar o registro requisitado.");
      return;
    }

    screen.ShowMessage($"O registro \"{selectedId}\" foi editado com sucesso.");
  }

  public void Delete()
  {

    screen.OperationHeader("Exclusão de Caixa");

    ShowAll(showHeader: false);

    string? selectedId = GetID();

    bool success = repository.Delete(selectedId);

    if (!success)
    {
      screen.ShowMessage("Não foi possível encontrar o registro requisitado.");
      return;
    }

    screen.ShowMessage($"O registro \"{selectedId}\" foi excluído com sucesso.");
  }

  private string GetID()
  {
    string? selectedId;

    do
    {
      Console.WriteLine("\nDigite o ID da caixa");
      Console.Write("> ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;
    } while (true);

    return selectedId;
  }

  private Box GetRegistrationData()
  {

    string? label;

    do
    {

      Console.WriteLine("\nInforme a etiqueta da caixa");
      Console.Write("> ");
      label = Console.ReadLine()?.ToUpper(); ;

      if (repository.FindByLabel(label!) == null) break;

      Console.WriteLine("\n⚠️ Já existe um registro de caixa com esta etiqueta.");
    } while (true);

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\n[1] Vermelho");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("[2] Verde");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("[3] Azul");
    Console.ResetColor();
    Console.WriteLine("[4] Branco (Padrão)");

    Console.WriteLine("\nSelecione uma das cores acima");
    Console.Write("> ");
    string? colorOption = Console.ReadLine();

    string? color;

    if (colorOption == "1") color = "Vermelho";
    else if (colorOption == "2") color = "Verde";
    else if (colorOption == "3") color = "Azul";
    else color = "Branco";

    Console.WriteLine("\nInforme o tempo de empréstimo das revistas da caixa");
    Console.Write("> ");
    int loanDays = Convert.ToInt32(Console.ReadLine());

    return new Box(label!, color, loanDays);
  }
}