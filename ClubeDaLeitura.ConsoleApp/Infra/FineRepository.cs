using ClubeDaLeitura.ConsoleApp.Domain.Fine;

namespace ClubeDaLeitura.ConsoleApp.Infra;

public class FineRepository
{

  private readonly List<Fine> fines = [];

  public void Create(Fine fine)
  {

    fines.Add(fine);
  }

  public List<Fine> FindAll()
  {

    return fines;
  }

  public Fine? FindById(string? id)
  {

    return fines.Find(f => f.Id == id);
  }

  public List<Fine> GetByFriendId(string friendId)
  {
    return fines
        .Where(f => f.Loan.Friend.Id == friendId)
        .ToList();
  }

  public List<Fine> GetByOpenStatus()
  {

    return fines
        .Where(f => f.Status == FineStatus.Pending)
        .ToList();
  }

  public bool HasFine(string loanId)
  {

    return fines.Any(f => f.Loan.Id == loanId);
  }
}