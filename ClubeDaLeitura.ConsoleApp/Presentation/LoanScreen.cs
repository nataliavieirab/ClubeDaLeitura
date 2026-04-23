using ClubeDaLeitura.ConsoleApp.Domain;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class LoanScreen
{
  private readonly ScreenUtils screen = new("Gestão de Empréstimos");

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

    Loan? newLoan = GetRegistrationData();

    string[] errors = newLoan!.Validate();

    if (errors.Length > 0)
    {
      screen.ShowError(errors);

      Open();
      return;
    }

    newLoan.Open();

  }

  private Loan? GetRegistrationData()
  {
    return null;
  }

}