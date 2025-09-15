using cp4_dotnet.Models.Entities;
using cp4_dotnet.Models.Interfaces;

namespace cp4_dotnet.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public double CalcularArea(object forma)
        {
            if (forma is ICalculo2D f2D)
                return f2D.CalcularArea();
            throw new InvalidOperationException("Não é possível calcular área para esta forma.");
        }

        public double CalcularPerimetro(object forma)
        {
            if (forma is ICalculo2D f2D)
                return f2D.CalcularPerimetro();
            throw new InvalidOperationException("Não é possível calcular perímetro para esta forma.");
        }

        public double CalcularVolume(object forma)
        {
            if (forma is ICalculo3D f3D)
                return f3D.CalcularVolume();
            throw new InvalidOperationException("Não é possível calcular volume para esta forma.");
        }

        public double CalcularAreaSuperficial(object forma)
        {
            if (forma is ICalculo3D f3D)
                return f3D.CalcularAreaSuperficial();
            throw new InvalidOperationException("Não é possível calcular área superficial para esta forma.");
        }

        public bool ValidarContencao(object formaExterna, object formaInterna)
        {
            if (formaExterna is Retangulo retanguloExterno && formaInterna is Circulo circuloInterno)
            {
                return (2 * circuloInterno.Raio) <= Math.Min(retanguloExterno.Largura, retanguloExterno.Altura);
            }

            if (formaExterna is Circulo circuloExterno && formaInterna is Retangulo retanguloInterno)
            {
                double meiaDiagonal = Math.Sqrt(Math.Pow(retanguloInterno.Largura, 2) + Math.Pow(retanguloInterno.Altura, 2)) / 2;
                return meiaDiagonal <= circuloExterno.Raio;
            }

            throw new NotSupportedException("A validação de contenção para esta combinação de formas não é suportada.");
        }
    }
}
