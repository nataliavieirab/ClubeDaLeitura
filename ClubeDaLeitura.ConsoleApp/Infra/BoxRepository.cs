using ClubeDaLeitura.ConsoleApp.Domain;
namespace ClubeDaLeitura.ConsoleApp.Infra;

public class BoxRepository : DefaultRepository<Box>
{
  public Box? FindByLabel(string label)
  {
    Box[] boxes = [.. FindAll()];

    for (int i = 0; i < boxes.Length; i++)
    {

      Box box = boxes[i];

      if (boxes[i].Label == label)
        return box;
    }

    return null;
  }
}