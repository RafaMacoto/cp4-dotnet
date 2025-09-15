using cp4_dotnet.Models.Interfaces;

namespace cp4_dotnet.Models.Entities
{
    public class Retangulo : ICalculo2D
    {
        public double Largura { get; set; }
        public double Altura { get; set; }

        public Retangulo() { }

        public Retangulo(double largura, double altura)
        {
            if (largura <= 0 || altura <= 0) throw new ArgumentException("Dimensões devem ser maiores que zero.");
            Largura = largura;
            Altura = altura;
        }

        public double CalcularArea() => Largura * Altura;

        public double CalcularPerimetro() => 2 * (Largura + Altura);
    }
}
