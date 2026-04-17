using System.Security.Cryptography;
namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Box : DefaultEntity
{
  public string Label { get; set; }
  public string Color { get; set; }
  public int LoanDays { get; set; } = 7;

  public Box(string label, string color, int loanDays)
  {

    Label = label;
    Color = color;
    LoanDays = loanDays;
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

  public void UpdateRegister(Box updatedBox)
  {

    Label = updatedBox.Label;
    Color = updatedBox.Color;
    LoanDays = updatedBox.LoanDays;
  }
}