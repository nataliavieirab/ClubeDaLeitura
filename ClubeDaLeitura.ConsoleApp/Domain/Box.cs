using System.Security.Cryptography;
namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Box
{
  public string Id { get; set; } = string.Empty;
  public string Label { get; set; } = string.Empty;
  public string Color { get; set; } = string.Empty;
  public int LoanDays { get; set; } = 7;

  public Box(string label, string color, int loanDays)
  {
    Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(20))
            .ToLower()
            .Substring(0, 7);

    Label = label;
    Color = color;
    LoanDays = loanDays;
  }
}
