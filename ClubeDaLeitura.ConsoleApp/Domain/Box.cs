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

  public void UpdateRegister(Box updatedBox)
  {
    Label = updatedBox.Label;
    Color = updatedBox.Color;
    LoanDays = updatedBox.LoanDays;
  }

  public string[] Validate()
  {
    string errors = string.Empty;

    if (string.IsNullOrWhiteSpace(Label))
      errors += "⚠️   O campo \"Etiqueta\" é obrigatório;";

    else if (Label.Length > 50)
      errors += "⚠️   O campo \"Etiqueta\" deve conter no máximo 50 caracteres;";

    if (LoanDays < 1)
      errors += "⚠️   O campo \"Tempo de Empréstimo\" deve conter um valor maior que 0;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }
}