using ClubeDaLeitura.ConsoleApp.Domain;

namespace ClubeDaLeitura.ConsoleApp.Infra;

public class LoanRepository
{

  private readonly List<Loan> loans = [];

  public void Create(Loan loan)
  {
    loans.Add(loan);
  }


}