namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class LoanScreen
{
  private readonly ScreenUtils screen = new("Gestão de Empréstimos");

  public string? GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine($"\n[1] Abrir");
    Console.WriteLine($"[2] Concluir");
    Console.WriteLine($"[3] Visualizar");
    Console.WriteLine($"[S] Voltar para o início");
    Console.Write("\n> ");
    string? mainOption = Console.ReadLine()?.ToUpper();

    return mainOption;
  }



}