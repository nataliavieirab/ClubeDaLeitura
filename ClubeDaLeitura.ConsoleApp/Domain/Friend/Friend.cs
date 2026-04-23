namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Friend : DefaultEntity<Friend>
{
  public string Name { get; set; } = string.Empty;
  public string GuardianName { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public List<Loan> loans = [];

  public Friend(string name, string guardianName, string phoneNumber)
  {
    this.Name = name;
    this.GuardianName = guardianName;
    this.PhoneNumber = phoneNumber;
  }

  public override string[] Validate()
  {
    string errors = string.Empty;

    if (string.IsNullOrWhiteSpace(Name))
      errors += "O campo \"Nome\" deve ser preenchido;";

    else if (Name.Length < 2 || Name.Length > 100)
      errors += "O campo \"Nome\" deve conter entre 2 e 100 caracteres;";

    if (string.IsNullOrWhiteSpace(GuardianName))
      errors += "O campo \"Nome do Responsável\" deve ser preenchido;";

    else if (GuardianName.Length < 2 || GuardianName.Length > 100)
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

    Name = updatedFriend.Name;
    GuardianName = updatedFriend.GuardianName;
    PhoneNumber = updatedFriend.PhoneNumber;
  }

  private (int digitCount, bool hasInvalidChar) AnalyzePhoneNumber()
  {
    int digitCount = 0;
    bool hasInvalidChar = false;

    string phoneDigits = PhoneNumber.Replace(" ", "").Replace("-", "");

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

  public void AddLoan(Loan loan)
  {
    loans.Add(loan);
  }
}