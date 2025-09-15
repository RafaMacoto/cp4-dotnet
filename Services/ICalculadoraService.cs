namespace cp4_dotnet.Services
{
    public interface ICalculadoraService
    {
        double CalcularArea(object forma);
        double CalcularPerimetro(object forma);
        double CalcularVolume(object forma);
        double CalcularAreaSuperficial(object forma);
        bool ValidarContencao(object formaExterna, object formaInterna);
    }
}
