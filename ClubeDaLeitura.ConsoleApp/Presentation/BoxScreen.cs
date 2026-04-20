using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class BoxScreen : DefaultScreen<Box>
{

  private readonly ScreenUtils screen = new("Gestão de Caixa");
  private readonly BoxRepository repository;

  public BoxScreen(BoxRepository repository) : base("Caixa", repository)
  {

    this.repository = repository;
  }

  public override void ShowAll(bool showHeader)
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

      string selectedColor = box.Color;

      if (selectedColor == "Vermelho")
        Console.ForegroundColor = ConsoleColor.Red;

      else if (selectedColor == "Verde")
        Console.ForegroundColor = ConsoleColor.Green;

      else if (selectedColor == "Azul")
        Console.ForegroundColor = ConsoleColor.Blue;

      Console.WriteLine(
          "{0, -7} | {1, -20} | {2, -10} | {3, -20}",
          box.Id, box.Label, box.Color, box.LoanDays
      );

      Console.ResetColor();
    }

    Console.WriteLine(line);

    if (showHeader)
    {
      Console.Write("\nDigite ENTER para continuar... ");
      Console.ReadLine();
    }
  }

  protected override Box GetRegistrationData()
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