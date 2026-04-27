using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Domain.Fine;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation.Default;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class FineScreen : IScreen
{
  private readonly ScreenUtils screen = new("Gestão de Multas");
  private readonly LoanRepository loanRepository = new();
  private readonly FineRepository repository = new();
  private readonly FriendRepository friendRepository = new();

  public FineScreen(FineRepository repository, LoanRepository loanRepository, FriendRepository friendRepository)
  {

    this.repository = repository;
    this.loanRepository = loanRepository;
    this.friendRepository = friendRepository;
  }

  public string GetMenuOption()
  {

    GenerateFinesIfNeeded();

    screen.MainHeader();
    Console.WriteLine($"\n[1] Visualizar multas");
    Console.WriteLine($"[2] Visualizar multas em aberto");
    Console.WriteLine($"[3] Visualizar multas por amigo");
    Console.WriteLine($"[4] Quitar multa");
    Console.WriteLine($"[S] Voltar para o início");
    Console.Write("\n> ");
    string? input = Console.ReadLine()?.ToUpper();

    return string.IsNullOrWhiteSpace(input)
        ? string.Empty
        : input.ToUpper();
  }

  public void HandleOption(string option)
  {

    if (option == "1") ShowAll(true);

    else if (option == "2") ShowAllPerPending(true);

    else if (option == "3") ShowAllPerFriends(true);

    else if (option == "4") Conclude();
  }

  public void GenerateFinesIfNeeded()
  {
    var loans = loanRepository.FindAll();

    foreach (var loan in loans)
    {
      if (loan.IsLate && !repository.HasFine(loan.Id))
      {
        Fine fine = new(loan);

        fine.SetPending();
        fine.CalculateAmount();

        repository.Create(fine);
      }
    }
  }

  public void Conclude()
  {

    screen.OperationHeader("Quitar Multas");

    ShowAllPerPending(false);

    Fine? fine;

    do
    {
      string? fineId = screen.GetEntityID("multa");

      fine = repository.FindById(fineId);

    } while (fine == null);

    ShowFineData(fine);

    Console.Write("\nDeseja realmente quitar a multa selecionado? [S/N]: ");
    string? option = Console.ReadLine()?.ToUpper();

    if (option != "S")
    {
      screen.ShowEnterMessage();
      return;
    }

    fine.Pay();

    screen.ShowMessage($"✅ A multa #{fine.Id} foi quitada com sucesso!");
  }

  public void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Multas");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -10} | {2, -15} | {3, -7} | {4, -10}",
        "Id", "Amigo", "Revista", "Valor", "Status"
    );

    Fine?[] fines = [.. repository.FindAll()];

    for (int i = 0; i < fines.Length; i++)
    {
      Fine? f = fines[i];

      if (f == null)
        continue;

      Console.Write("{0, -7} | ", f.Id);
      Console.Write("{0, -10} | ", f.Loan.Friend.Name);
      Console.Write("{0, -15} | ", f.Loan.Magazine.Title);
      Console.Write("{0, -7} | ", f.Amount);

      string status = string.Empty;

      if (f.Status == FineStatus.Undefined)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
        status = "Indefinido";
      }
      else if (f.Status == FineStatus.Pending)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        status = "Pendente";
      }
      else if (f.Status == FineStatus.Paid)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        status = "Pago";
      }

      Console.Write("{0, -10}", status);

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

  public void ShowAllPerPending(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Multas em Aberto");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
    "\n{0, -7} | {1, -10} | {2, -15} | {3, -7} | {4, -7}",
    "Id", "Amigo", "Revista", "Valor", "Status"
    );

    Fine?[] fines = [.. repository.GetByOpenStatus()];

    for (int i = 0; i < fines.Length; i++)
    {
      Fine? f = fines[i];

      if (f == null)
        continue;

      Console.Write("{0, -7} | ", f.Id);
      Console.Write("{0, -10} | ", f.Loan.Friend.Name);
      Console.Write("{0, -15} | ", f.Loan.Magazine.Title);
      Console.Write("{0, -7} | ", f.Amount);

      string status = "Pendente";

      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("{0, -7}", status);

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

  public void ShowAllPerFriends(bool showHeader)
  {

    if (showHeader) screen.OperationHeader("Multas por Amigo");

    ShowFriends();

    Friend? friend;

    do
    {
      string? friendId = screen.GetEntityID("amigo");

      friend = friendRepository.FindById(friendId);

    } while (friend == null);

    screen.OperationHeader($"Multas de {friend.Name}");
    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
    "\n{0, -7} | {1, -10} | {2, -15} | {3, -7} | {4, -7}",
    "Id", "Amigo", "Revista", "Valor", "Status"
    );

    Fine?[] finesPerFriend = [.. repository.GetByFriendId(friend.Id)];

    for (int i = 0; i < finesPerFriend.Length; i++)
    {
      Fine? f = finesPerFriend[i];

      if (f == null)
        continue;

      Console.Write("{0, -7} | ", f.Id);
      Console.Write("{0, -10} | ", f.Loan.Friend.Name);
      Console.Write("{0, -15} | ", f.Loan.Magazine.Title);
      Console.Write("{0, -7} | ", f.Amount);

      string status = "Pendente";

      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("{0, -7}", status);

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

  public void ShowFineData(Fine fine)
  {

    string line = screen.GetUIDoubleLine();

    Console.WriteLine("\n>> Multa selecionada:");
    Console.Write($"{line}");
    Console.WriteLine(
    "\n{0, -7} | {1, -15} | {2, -15} | {3, -7} ",
    "Id", "Amigo", "Revista", "Valor"
    );
    Console.WriteLine(
        "{0, -7} | {1, -15} | {2, -15} | {3, -7}",
        fine.Id, fine.Loan.Friend.Name, fine.Loan.Magazine.Title, fine.Amount
    );
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