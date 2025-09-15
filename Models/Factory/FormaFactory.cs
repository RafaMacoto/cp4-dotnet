using cp4_dotnet.DTOs;
using cp4_dotnet.Models.Entities;     // <- Circulo, Retangulo, Esfera
using cp4_dotnet.Models.Interfaces;   // <- IFormaFactory

namespace cp4_dotnet.Models.Factory
{
    public class FormaFactory : IFormaFactory
    {
        public object CriarForma(FormaRequest dto)
        {
            if (dto.Parametros == null || dto.Parametros.Any(p => p.Value <= 0))
            {
                throw new ArgumentException("As dimensões da forma devem ser valores positivos.");
            }
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Tipo))
                throw new ArgumentException("O tipo da forma é obrigatório.", nameof(dto.Tipo));

            switch (dto.Tipo.Trim().ToLowerInvariant())
            {
                case "circulo":
                    if (dto.Parametros.TryGetValue("raio", out var raioCirculo))
                        return new Circulo(raioCirculo);
                    throw new ArgumentException("Parâmetro 'raio' é obrigatório para círculo.", nameof(dto.Parametros));

                case "retangulo":
                    if (dto.Parametros.TryGetValue("largura", out var largura) &&
                        dto.Parametros.TryGetValue("altura", out var altura))
                        return new Retangulo(largura, altura);
                    throw new ArgumentException("Parâmetros 'largura' e 'altura' são obrigatórios para retângulo.", nameof(dto.Parametros));

                case "esfera":
                    if (dto.Parametros.TryGetValue("raio", out var raioEsfera))
                        return new Esfera(raioEsfera);
                    throw new ArgumentException("Parâmetro 'raio' é obrigatório para esfera.", nameof(dto.Parametros));

                default:
                    throw new ArgumentException($"Forma desconhecida: {dto.Tipo}");
            }
        }
    }
}
