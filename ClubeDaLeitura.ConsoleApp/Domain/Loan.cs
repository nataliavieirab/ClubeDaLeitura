using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Loan
{
  public string Id { get; set; } = string.Empty;
  public Magazine Magazine { get; set; }
  public Friend Friend { get; set; }
  public DateTime Abertura { get; set; }
  public DateTime ConclusaoPrevista { get; set; }
  public LoanStatus Status { get; set; } = LoanStatus.Undefined;

  public Loan(Magazine magazine, Friend friend)
  {
    Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(20))
            .ToLower()
            .Substring(0, 7);

    Magazine = magazine;
    Friend = friend;
  }

  public string[] Validate()
  {
    string errors = string.Empty;

    if (Magazine == null)
      errors = "O campo \"Revista\" deve ser preenchido;";

    if (Friend == null)
      errors = "O campo \"Amigo\" deve ser preenchido;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }
}
