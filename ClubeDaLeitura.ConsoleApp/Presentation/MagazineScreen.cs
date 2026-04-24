using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class MagazineScreen : DefaultScreen<Magazine>
{

  private readonly ScreenUtils screen = new("Gestão de Revistas");
  public MagazineRepository repository;
  public BoxRepository boxRepository;
  public BoxScreen boxScreen;

  public MagazineScreen(MagazineRepository _repository, BoxRepository _boxRepository, BoxScreen _boxScreen) : base("Revista", _repository)
  {

    repository = _repository;
    boxRepository = _boxRepository;
    boxScreen = _boxScreen;
  }

  public override void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Revistas");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15}",
        "ID", "Título", "Edição", "Ano", "Caixa"
    );

    Magazine?[] magazines = [.. repository.FindAll()];

    for (int i = 0; i < magazines.Length; i++)
    {
      Magazine? m = magazines[i];

      if (m == null)
        continue;

      Console.Write("{0, -7} | ", m.Id);
      Console.Write("{0, -25} | ", m.Title);
      Console.Write("{0, -6} | ", m.NumberEdition);
      Console.Write("{0, -4} | ", m.ReleaseYear);

      string selectedColor = m.Box.Color;

      if (selectedColor == "Vermelho")
        Console.ForegroundColor = ConsoleColor.Red;

      else if (selectedColor == "Verde")
        Console.ForegroundColor = ConsoleColor.Green;

      else if (selectedColor == "Azul")
        Console.ForegroundColor = ConsoleColor.Blue;

      Console.Write("{0, -15}", m.Box.Label);

      Console.ResetColor();
      Console.WriteLine();
    }

    Console.WriteLine(line);

    if (showHeader)
    {
      Console.Write("\nDigite ENTER para continuar... ");
      Console.ReadLine();
    }
  }

  protected override Magazine GetRegistrationData()
  {

    Console.WriteLine("\nInforme o título da revista");
    Console.Write("> ");
    string? title = Console.ReadLine()?.ToUpper();

    Console.WriteLine("\nInforme o número de edição da revista");
    Console.Write("> ");
    int numberEdition = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("\nInforme o ano de lançamento da revista");
    Console.Write("> ");
    int releaseYear = Convert.ToInt32(Console.ReadLine());

    Box box = SelectBox();

    return new Magazine(title!, numberEdition, releaseYear, box);
  }

  private Box SelectBox()
  {

    screen.OperationHeader("GUARDAR REVISTA");
    Console.WriteLine("\n📋 LISTA DE CAIXAS:");

    boxScreen.ShowAll(showHeader: false);

    string? boxId;
    Box? box;

    do
    {
      Console.Write("\nDigite o ID da caixa em que deseja guardar a revista: ");
      boxId = Console.ReadLine();

      box = boxRepository.FindById(boxId);

      if (!string.IsNullOrWhiteSpace(boxId) && boxId.Length == 7 && box != null)
        break;

    } while (true);

    return box;
  }
}