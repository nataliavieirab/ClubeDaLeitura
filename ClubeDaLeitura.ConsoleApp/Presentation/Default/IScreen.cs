namespace ClubeDaLeitura.ConsoleApp.Presentation.Default;

public interface IScreen
{
  string GetMenuOption();
  void HandleOption(string option);
}