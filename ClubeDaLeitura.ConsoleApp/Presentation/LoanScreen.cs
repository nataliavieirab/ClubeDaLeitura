using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation.Default;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class LoanScreen : IScreen
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

  public string GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine($"\n[1] Abrir");
    Console.WriteLine($"[2] Concluir");
    Console.WriteLine($"[3] Visualizar");
    Console.WriteLine($"[S] Voltar para o início");
    Console.Write("\n> ");
    string? input = Console.ReadLine()?.ToUpper();

    return string.IsNullOrWhiteSpace(input)
        ? string.Empty
        : input.ToUpper();
  }

  public void HandleOption(string option)
  {

    if (option == "1")
      Open();

    else if (option == "2")
      Complete();

    else if (option == "3")
      ShowAll(true);
  }

  public void Open()
  {

    screen.OperationHeader("Abertura de Empréstimo");

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

  public void Complete()
  {
    screen.OperationHeader("Conclusão de Empréstimo");

    ShowAll(showHeader: false);

    Loan? loan = null;

    do
    {
      string? loanId = screen.GetEntityID("empréstimo");

      loan = repository.FindById(loanId);

    } while (loan == null);

    ShowLoanData(loan);

    Console.Write("\nDeseja realmente concluir o empréstimo selecionado? [S/N]: ");
    string? option = Console.ReadLine()?.ToUpper();

    if (option != "S")
    {
      screen.ShowEnterMessage();
      return;
    }

    loan.Complete();

    screen.ShowMessage($"✅ O empréstimo #{loan.Id} foi concluído com sucesso!");
  }

  public void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Empréstimos");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15} | {5, -10}",
        "Id", "Revista", "Amigo", "Abertura", "Conclusão Prev.", "Status"
    );

    Loan?[] loans = [.. repository.FindAll()];

    for (int i = 0; i < loans.Length; i++)
    {
      Loan? l = loans[i];

      if (l == null)
        continue;

      Console.Write("{0, -7} | ", l.Id);
      Console.Write("{0, -15} | ", l.Magazine.Title);
      Console.Write("{0, -10} | ", l.Friend.Name);
      Console.Write("{0, -10} | ", l.OpenDate.ToShortDateString());
      Console.Write("{0, -15} | ", l.DueDate.ToShortDateString());

      string status = l.Status.ToString();

      if (l.IsLate)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        status = "Atrasado";
      }
      else if (l.Status == LoanStatus.Undefined)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
        status = "Indefinido";
      }
      else if (l.Status == LoanStatus.Open)
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        status = "Aberto";
      }
      else if (l.Status == LoanStatus.Completed)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        status = "Concluído";
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

  public void ShowLoanData(Loan loan)
  {

    string line = screen.GetUIDoubleLine();

    Console.WriteLine("\n>> Empréstimo selecionado:");
    Console.Write($"{line}");
    Console.WriteLine(
    "\n{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15}",
    "ID", "Revista", "Amigo", "Abertura", "Conclusão Prev.");
    Console.WriteLine(
        "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15}",
        loan.Id, loan.Magazine.Title, loan.Friend.Name, loan.OpenDate.ToShortDateString(), loan.DueDate.ToShortDateString()
    );
    Console.WriteLine(line);
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