using ClubeDaLeitura.ConsoleApp.Domain;

namespace ClubeDaLeitura.ConsoleApp.Infra;

public class MagazineRepository
{
  private readonly List<Magazine> magazines = [];

  public void Create(Magazine newMagazine)
  {
    magazines.Add(newMagazine);
  }

  public List<Magazine> FindAll()
  {
    return magazines;
  }
}
