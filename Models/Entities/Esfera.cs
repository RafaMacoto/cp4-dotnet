using cp4_dotnet.Models.Interfaces;

namespace cp4_dotnet.Models.Entities
{
    public class Esfera : ICalculo3D
    {
        public double Raio { get; }

        public Esfera(double raio)
        {
            if (raio <= 0) throw new ArgumentException("O raio deve ser maior que zero.");
            Raio = raio;
        }

        public double CalcularVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(Raio, 3);
        public double CalcularAreaSuperficial() => 4 * Math.PI * Math.Pow(Raio, 2);
    }
}
