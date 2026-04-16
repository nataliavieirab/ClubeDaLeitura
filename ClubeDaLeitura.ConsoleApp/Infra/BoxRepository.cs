using ClubeDaLeitura.ConsoleApp.Domain;
namespace ClubeDaLeitura.ConsoleApp.Infra;

public class BoxRepository
{
  private readonly List<Box> boxes = [];

  public void Create(Box newBox)
  {

    boxes.Add(newBox);
  }

  public bool Update(string id, Box newBox)
  {

    Box? box = FindById(id);

    if (box == null) return false;

    box.Label = newBox.Label;
    box.Color = newBox.Color;
    box.LoanDays = newBox.LoanDays;

    return true;
  }

  public bool Delete(string id)
  {
    Box? box = FindById(id);

    if (box == null) return false;

    boxes.Remove(box);

    return true;
  }
  public List<Box> FindAll()
  {

    return boxes;
  }

  public Box? FindById(string id)
  {

    return boxes.Find(b => b.Id == id);
  }
}