using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class BoxScreen
{

  private readonly ScreenUtils screen = new("Gestão de Caixa");
  private readonly BoxRepository repository;

  public BoxScreen(BoxRepository _repository)
  {
    repository = _repository;
  }
  public string? GetMenuOption()
  {
    screen.MainHeader();
    Console.WriteLine("\n[1] - Cadastrar Caixa");
    Console.WriteLine("[2] - Editar Caixa");
    Console.WriteLine("[3] - Excluir Caixa");
    Console.WriteLine("[4] - Visualizar Caixas");
    Console.WriteLine("[S] - Voltar para o início");
    screen.ShowUISimpleLine();
    Console.Write("\n> ");
    string? mainOption = Console.ReadLine()?.ToUpper();

    return mainOption;
  }

  public void Register()
  {

    screen.OperationHeader("Cadastro de Caixa");

    Box newBox = GetRegistrationData();

    repository?.Create(newBox);

    screen.ShowMessage($"A caixa \"{newBox.Id}\" foi cadastrada com sucesso!");
  }

  private Box GetRegistrationData()
  {

    Console.Write("Informe a etiqueta da caixa: ");
    string? label = Console.ReadLine();

    Console.WriteLine("\n>> Selecione uma das cores abaixo");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("1 - Vermelho");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("2 - Verde");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("3 - Azul");
    Console.ResetColor();
    Console.WriteLine("4 - Branco (Padrão)");

    Console.Write("\n> ");
    string? colorOption = Console.ReadLine();

    string? color;

    if (colorOption == "1") color = "Vermelho";
    else if (colorOption == "2") color = "Verde";
    else if (colorOption == "3") color = "Azul";
    else color = "Branco";

    Console.Write("Informe o tempo de empréstimo das revistas da caixa: ");
    int loanDays = Convert.ToInt32(Console.ReadLine());

    return new Box(label!, color, loanDays);
  }
}
