using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation.Default;


namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class MainScreen
{

  private readonly ScreenUtils screen = new("Clube da Leitura");
  private BoxRepository boxRepository;
  private MagazineRepository magazineRepository;
  private FriendRepository friendRepository;
  private LoanRepository loanRepository;
  private FineRepository fineRepository;

  public MainScreen
  (
      BoxRepository boxRepository,
      MagazineRepository magazineRepository,
      FriendRepository friendRepository,
      LoanRepository loanRepository,
      FineRepository fineRepository
  )
  {
    this.boxRepository = boxRepository;
    this.magazineRepository = magazineRepository;
    this.friendRepository = friendRepository;
    this.loanRepository = loanRepository;
    this.fineRepository = fineRepository;

    Box box = new Box("Lançamentos", "Vermelho", 3);
    boxRepository.Create(box);

    Magazine magazine = new Magazine("Action Comics", 155, 1990, box);
    magazineRepository.Create(magazine);

    Friend friend = new Friend("Joãozinho", "Dona Cleide", "49 98222-4353");
    friendRepository.Create(friend);

    Loan loan = new Loan(magazine, friend);
    loan.Open();
    loan.OpenDate = DateTime.Now.AddDays(-7);
    loanRepository.Create(loan);
  }

  public IScreen? GetMainMenuOption()
  {
    screen.MainHeader();
    Console.WriteLine("\n[1] Gerenciar Caixas de Revistas");
    Console.WriteLine("[2] Gerenciar Revistas");
    Console.WriteLine("[3] Gerenciar Amigos");
    Console.WriteLine("[4] Gerenciar Empréstimos");
    Console.WriteLine("[5] Gerenciar Multas");
    Console.WriteLine("[S] Sair");
    Console.Write("\n> ");
    string menuOption = Console.ReadLine()?.ToUpper()!;

    if (menuOption == "1")
      return new BoxScreen(boxRepository);

    if (menuOption == "2")
      return new MagazineScreen(magazineRepository, boxRepository);

    if (menuOption == "3")
      return new FriendScreen(friendRepository);

    if (menuOption == "4")
      return new LoanScreen(loanRepository, magazineRepository, friendRepository);

    if (menuOption == "5")
      return new FineScreen(fineRepository, loanRepository);

    return null;
  }
}