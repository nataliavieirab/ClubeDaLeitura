using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation;
using ClubeDaLeitura.ConsoleApp.Presentation.Default;

BoxRepository boxRepository = new();
MagazineRepository magazineRepository = new();
FriendRepository friendRepository = new();
LoanRepository loanRepository = new();
FineRepository fineRepository = new();

MainScreen mainScreen = new(boxRepository, magazineRepository, friendRepository, loanRepository, fineRepository);

while (true)
{
  IScreen? selectedScreen = mainScreen.GetMainMenuOption();

  if (selectedScreen == null)
  {
    Console.Clear();
    break;
  }

  while (true)
  {
    string option = selectedScreen.GetMenuOption();

    if (option == "S")
      break;

    selectedScreen.HandleOption(option);
  }
}