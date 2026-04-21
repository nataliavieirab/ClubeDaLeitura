using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;
namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class FriendScreen : DefaultScreen<Friend>
{
  private readonly ScreenUtils screen = new("Gestão de Amigos");
  private readonly FriendRepository repository;

  public FriendScreen(FriendRepository repository) : base("Amigo", repository)
  {

    this.repository = repository;
  }

  public override void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Amigos");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -15} | {2, -15} | {3, -13}",
        "ID", "Nome", "Responsável", "Telefone"
    );

    Friend?[] friends = [.. repository.FindAll()];

    for (int i = 0; i < friends.Length; i++)
    {
      Friend? f = friends[i];

      if (f == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
          f.Id, f.name, f.guardianName, f.phoneNumber
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

  protected override Friend GetRegistrationData()
  {
    Console.Write("\nDigite o nome: ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o nome do responsável: ");
    string guardianName = Console.ReadLine() ?? string.Empty;

    Console.Write("Digite o telefone: ");
    string phoneNumber = Console.ReadLine() ?? string.Empty;

    return new Friend(name, guardianName, phoneNumber);
  }
}