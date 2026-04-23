using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation;

// 1. Instanciação de dependências
BoxRepository boxRepository = new();
MagazineRepository magazineRepository = new();
FriendRepository friendRepository = new();
LoanRepository loanRepository = new();

BoxScreen boxScreen = new(boxRepository);
MagazineScreen magazineScreen = new(magazineRepository, boxRepository, boxScreen);
FriendScreen friendScreen = new(friendRepository);
LoanScreen loanScreen = new(loanRepository, magazineRepository, friendRepository);

// 2. Criação de dados teste
Box box = new("Lançamentos", "Vermelho", 3);
boxRepository.Create(box);

Magazine magazine = new("Action Comics", 155, 1990, box);
magazineRepository.Create(magazine);

Friend friend = new("Savarininho", "Savarino", "48 99976 5544");
friendRepository.Create(friend);

Loan loan = new(magazine, friend);
loan.Open();

ScreenUtils screen = new("Clube da Leitura");

while (true)
{
  string mainMenuOption = screen.GetMainMenuOption();

  if (mainMenuOption == "S")
  {
    Console.Clear();
    break;
  }

  while (true)
  {
    string? innerMenuOption = string.Empty;

    if (mainMenuOption == "1") // Boxes
    {
      innerMenuOption = boxScreen.GetMenuOption();

      if (innerMenuOption == "S")
      {
        Console.Clear();
        break;
      }

      if (innerMenuOption == "1") boxScreen.Register();

      else if (innerMenuOption == "2") boxScreen.Edit();

      else if (innerMenuOption == "3") boxScreen.Delete();

      else if (innerMenuOption == "4") boxScreen.ShowAll(showHeader: true);
    }

    else if (mainMenuOption == "2") // Magazines
    {
      innerMenuOption = magazineScreen.GetMenuOption();

      if (innerMenuOption == "S")
      {
        Console.Clear();
        break;
      }

      if (innerMenuOption == "1") magazineScreen.Register();

      else if (innerMenuOption == "2") magazineScreen.Edit();

      else if (innerMenuOption == "3") magazineScreen.Delete();

      else if (innerMenuOption == "4") magazineScreen.ShowAll(showHeader: true);
    }

    else if (mainMenuOption == "3") // Friends
    {
      innerMenuOption = friendScreen.GetMenuOption();

      if (innerMenuOption == "S")
      {
        Console.Clear();
        break;
      }

      if (innerMenuOption == "1") friendScreen.Register();

      else if (innerMenuOption == "2") friendScreen.Edit();

      else if (innerMenuOption == "3") friendScreen.Delete();

      else if (innerMenuOption == "4") friendScreen.ShowAll(showHeader: true);
    }

    else if (mainMenuOption == "4") // Loans
    {
      innerMenuOption = loanScreen.GetMenuOption();

      if (innerMenuOption == "S")
      {
        Console.Clear();
        break;
      }

      if (innerMenuOption == "1") loanScreen.Open();

      // else if (innerMenuOption == "2") loanScreen.();

      // else if (innerMenuOption == "3") loanScreen.ShowAll(showHeader: true);
    }
  }
}