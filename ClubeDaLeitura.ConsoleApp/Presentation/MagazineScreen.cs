using ClubeDaLeitura.ConsoleApp.Domain;
using ClubeDaLeitura.ConsoleApp.Infra;

namespace ClubeDaLeitura.ConsoleApp.Presentation;

public class MagazineScreen
{

  private readonly ScreenUtils screen = new("Gestão de Revistas");
  public MagazineRepository repository;
  public BoxRepository boxRepository;
  public BoxScreen boxScreen;

  public MagazineScreen(MagazineRepository _repository, BoxRepository _boxRepository, BoxScreen _boxScreen)
  {

    repository = _repository;
    boxRepository = _boxRepository;
    boxScreen = _boxScreen;
  }

  public string? GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine("\n[1] Cadastrar Revista");
    Console.WriteLine("[2] Editar Revista");
    Console.WriteLine("[3] Excluir Revista");
    Console.WriteLine("[4] Visualizar Revistas");
    Console.WriteLine("[S] Voltar para o início");
    Console.Write("\n> ");
    string? mainOption = Console.ReadLine()?.ToUpper();

    return mainOption;
  }

  public void Register()
  {

    screen.OperationHeader("Cadastro de Revista");

    Magazine newMagazine = GetRegistrationData();

    string[] errors = newMagazine.Validate();

    if (errors.Length > 0)
    {

      screen.ShowError(errors);

      Register();
      return;
    }

    repository.Create(newMagazine);

    screen.ShowMessage($"✅ O registro \"{newMagazine.Id}\" foi cadastrado com sucesso!");
  }

  public Magazine GetRegistrationData()
  {

    Console.WriteLine("\nInforme o título da revista");
    Console.Write("> ");
    string? title = Console.ReadLine()?.ToUpper();

    Console.WriteLine("\nInforme o número de edição da revista");
    Console.Write("> ");
    int numberEdition = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("\nInforme o ano de lançamento da revista");
    Console.Write("> ");
    int releaseYear = Convert.ToInt32(Console.ReadLine());

    Box box = SelectBox();

    return new Magazine(title!, numberEdition, releaseYear, box);
  }

  private Box SelectBox()
  {

    screen.OperationHeader("GUARDAR REVISTA");

    Console.WriteLine("📋 LISTA DE CAIXAS:");
    boxScreen.ShowAll(showHeader: false);

    string? boxId;
    Box? box;

    do
    {
      Console.Write("\nDigite o ID da caixa em que deseja guardar a revista: ");
      boxId = Console.ReadLine();

      box = boxRepository.FindById(boxId);

      if (!string.IsNullOrWhiteSpace(boxId) && boxId.Length == 7 && box != null)
        break;

    } while (true);

    return box;
  }
}