using ClubeDaLeitura.ConsoleApp.Infra;
using ClubeDaLeitura.ConsoleApp.Presentation;

ScreenUtils screen = new("Clube da Leitura");

BoxRepository boxRepository = new();
BoxScreen boxScreen = new(boxRepository);

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

    if (mainMenuOption == "1")
    {
      innerMenuOption = boxScreen.GetMenuOption();

      if (innerMenuOption == "S")
      {
        Console.Clear();
        break;
      }

      if (innerMenuOption == "1") boxScreen.Register();

      //else if (innerMenuOption == "2") boxScreen.Edit();

      //else if (innerMenuOption == "3") boxScreen.Delete();

      else if (innerMenuOption == "4") boxScreen.ShowAll(showHeader: true);
    }

    else if (mainMenuOption == "2")
    {

    }

    else if (mainMenuOption == "3")
    {

    }

    else if (mainMenuOption == "4")
    {

    }
  }
}