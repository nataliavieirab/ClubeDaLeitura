using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Domain.Fine;

public class Fine
{

  public string Id { get; set; } = string.Empty;

  public Loan Loan { get; private set; }

  public decimal Amount { get; private set; }

  public FineStatus Status { get; private set; } = FineStatus.Undefined;

  public DateTime CreatedAt { get; private set; }

  public Fine(Loan loan)
  {

    Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(20))
            .ToLower()
            .Substring(0, 7);

    Loan = loan;
  }

  public string[] Validate()
  {

    string errors = string.Empty;

    if (Loan == null)
      errors = "O campo \"Loan\" deve ser preenchido;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }

  public void CalculateAmount()
  {
    int daysLate = Loan.CalculateDelayDays();

    Amount = daysLate * 2.0m;
  }


  public void Pay()
  {
    Status = FineStatus.Paid;
  }
}