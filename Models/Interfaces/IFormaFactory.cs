using cp4_dotnet.DTOs;

namespace cp4_dotnet.Models.Interfaces
{
    public interface IFormaFactory
    {
        object CriarForma(FormaRequest dto);
    }
}
