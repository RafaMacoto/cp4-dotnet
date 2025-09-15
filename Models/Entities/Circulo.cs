using cp4_dotnet.Models.Interfaces;



namespace cp4_dotnet.Models.Entities
{
    public class Circulo : ICalculo2D
    {
        public double Raio { get; set; }

        public Circulo() { }

        public Circulo(double raio)
        {
            if (raio <= 0) throw new ArgumentException("Raio deve ser maior que zero.");
            Raio = raio;
        }

        public double CalcularArea() => Math.PI * Raio * Raio;

        public double CalcularPerimetro() => 2 * Math.PI * Raio;
    }
}
