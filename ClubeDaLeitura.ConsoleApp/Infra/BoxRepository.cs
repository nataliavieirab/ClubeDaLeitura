using ClubeDaLeitura.ConsoleApp.Domain;

namespace ClubeDaLeitura.ConsoleApp.Infra;

public class BoxRepository
{
  private readonly List<Box> boxes = [];

  public void Create(Box newBox)
  {
    boxes.Add(newBox);
  }

  public List<Box> FindAll()
  {
    return boxes;
  }
}