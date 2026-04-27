using ClubeDaLeitura.ConsoleApp.Domain.Fine;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation.Default;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class FineScreen : IScreen
{
  private readonly ScreenUtils screen = new("Gestão de Multas");
  private readonly FineRepository repository = new();

  private readonly LoanRepository loanRepository = new();

  public FineScreen(FineRepository repository, LoanRepository loanRepository)
  {

    this.repository = repository;
    this.loanRepository = loanRepository;
  }

  public string GetMenuOption()
  {

    GenerateFinesIfNeeded();

    screen.MainHeader();
    Console.WriteLine($"\n[1] Visualizar multas em aberto");
    Console.WriteLine($"[2] Visualizar multas por amigo");
    Console.WriteLine($"[3] Quitar multa");
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
      ShowAllOpen(true);

    // else if (option == "2")
    //   ShowAllPerFriends();

    // else if (option == "3")
    //   Pay();
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

  public void ShowAllOpen(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Multas");

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
}