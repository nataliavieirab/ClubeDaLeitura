using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Domain;

public class DefaultEntity
{
  public string Id { get; set; } = string.Empty;

  public DefaultEntity()
  {

    Id = Convert
        .ToHexString(RandomNumberGenerator.GetBytes(20))
        .ToLower()
        .Substring(0, 7);
  }

  // public abstract void UpdateRegister(BaseEntity updatedEntity);
}