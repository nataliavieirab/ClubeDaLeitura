using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class LoanScreen
{
  private readonly ScreenUtils screen = new("Gestão de Empréstimos");
  private readonly LoanRepository repository = new();
  private readonly MagazineRepository magazineRepository = new();
  private readonly FriendRepository friendRepository = new();

  public LoanScreen(LoanRepository repository, MagazineRepository magazineRepository, FriendRepository friendRepository)
  {

    this.repository = repository;
    this.magazineRepository = magazineRepository;
    this.friendRepository = friendRepository;
  }

  public string? GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine($"\n[1] Abrir");
    Console.WriteLine($"[2] Concluir");
    Console.WriteLine($"[3] Visualizar");
    Console.WriteLine($"[S] Voltar para o início");
    Console.Write("\n> ");
    string? mainOption = Console.ReadLine()?.ToUpper();

    return mainOption;
  }

  public void Open()
  {

    screen.OperationHeader("Abertura de Emprestimo");

    Loan loan = GetRegistrationData();

    string[] errors = loan.Validate();

    if (errors.Length > 0)
    {
      screen.ShowError(errors);

      Open();
      return;
    }

    loan.Open();

    repository.Create(loan);

    screen.ShowMessage($"✅ O empréstimo #{loan.Id} foi aberto com sucesso!");
  }

  private Loan GetRegistrationData()
  {

    ShowMagazines();
    Magazine? magazine = null;

    do
    {
      string? magazineId = screen.GetEntityID("revista");

      magazine = magazineRepository.FindById(magazineId);

    } while (magazine == null);

    ShowFriends();
    Friend? friend = null;

    do
    {
      string? friendId = screen.GetEntityID("amigo");

      friend = friendRepository.FindById(friendId);

    } while (friend == null);

    return new Loan(magazine, friend);
  }

  private void ShowMagazines()
  {
    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15}",
        "ID", "Título", "Edição", "Ano", "Caixa"
    );

    Magazine?[] magazines = [.. magazineRepository.FindAll()];

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
  }

  private void ShowFriends()
  {
    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -15} | {2, -15} | {3, -13}",
        "ID", "Nome", "Responsável", "Telefone"
    );

    Friend?[] friends = [.. friendRepository.FindAll()];

    for (int i = 0; i < friends.Length; i++)
    {
      Friend? f = friends[i];

      if (f == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
          f.Id, f.Name, f.GuardianName, f.PhoneNumber
      );

      Console.ResetColor();
    }

    Console.WriteLine(line);
  }

}