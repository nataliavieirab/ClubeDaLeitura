namespace ClubeDaLeitura.ConsoleApp.Domain;

public class Magazine : BaseEntity
{
  public string Title { get; set; }
  public int NumberEdition { get; set; }
  public int ReleaseYear { get; set; }
  public Box Box { get; set; }

  public Magazine(string title, int numberEdition, int releaseYear, Box box)
  {

    Title = title;
    NumberEdition = numberEdition;
    ReleaseYear = releaseYear;
    Box = box;
  }

  public string[] Validate()
  {
    string errors = string.Empty;

    if (string.IsNullOrWhiteSpace(Title))
      errors += "⚠️   O campo \"Título\" é obrigatório;";

    if (Title.Length < 2 || Title.Length > 100)
      errors += "O campo \"Título\" deve conter entre 2 e 100 caracteres;";

    if (NumberEdition < 0)
      errors += "O campo \"Numero da Edição\" deve conter um valor igual ou maior que 0;";

    int currentYear = DateTime.Now.Year;

    if (ReleaseYear < 1 || ReleaseYear > currentYear)
      errors += "O campo \"Ano de Publicação\" deve conter uma data válida;";

    if (Box == null)
      errors += "O campo \"Caixa\" deve conter uma caixa válida;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }

  public void UpdateRegister(Magazine updatedMagazine)
  {

    Title = updatedMagazine.Title;
    NumberEdition = updatedMagazine.NumberEdition;
    ReleaseYear = updatedMagazine.ReleaseYear;
    Box = updatedMagazine.Box;
  }
}