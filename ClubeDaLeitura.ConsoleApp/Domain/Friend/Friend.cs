namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Friend : DefaultEntity<Friend>
{
  public string name { get; set; } = string.Empty;
  public string guardianName { get; set; } = string.Empty;
  public string phoneNumber { get; set; } = string.Empty;

  public Friend(string name, string guardianName, string phoneNumber)
  {
    this.name = name;
    this.guardianName = guardianName;
    this.phoneNumber = phoneNumber;
  }

  public override string[] Validate()
  {
    string errors = string.Empty;

    if (string.IsNullOrWhiteSpace(name))
      errors += "O campo \"Nome\" deve ser preenchido;";

    else if (name.Length < 2 || name.Length > 100)
      errors += "O campo \"Nome\" deve conter entre 2 e 100 caracteres;";

    if (string.IsNullOrWhiteSpace(guardianName))
      errors += "O campo \"Nome do Responsável\" deve ser preenchido;";

    else if (guardianName.Length < 2 || guardianName.Length > 100)
      errors += "O campo \"Nome\" deve conter entre 2 e 100 caracteres;";

    var (digitCount, hasInvalidChar) = AnalyzePhoneNumber();

    if (digitCount < 10 || digitCount > 11)
      errors += "O campo \"Telefone\" deve conter entre 10 e 11 dígitos;";

    if (hasInvalidChar)
      errors += "O campo \"Telefone\" deve conter apenas dígitos;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }

  public override void UpdateRegister(Friend updatedFriend)
  {

    name = updatedFriend.name;
    guardianName = updatedFriend.guardianName;
    phoneNumber = updatedFriend.phoneNumber;
  }

  private (int digitCount, bool hasInvalidChar) AnalyzePhoneNumber()
  {
    int digitCount = 0;
    bool hasInvalidChar = false;

    string phoneDigits = phoneNumber.Replace(" ", "").Replace("-", "");

    for (int i = 0; i < phoneDigits.Length; i++)
    {

      char currentChar = phoneDigits[i];

      if (char.IsDigit(currentChar))
      {
        digitCount++;
      }

      else
      {
        hasInvalidChar = true;
        break;
      }
    }

    return (digitCount, hasInvalidChar);
  }
}