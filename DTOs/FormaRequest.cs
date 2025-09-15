using System.ComponentModel.DataAnnotations;

namespace cp4_dotnet.DTOs
{
   
    public class FormaRequest
    {
        
        [Required]
        public string Tipo { get; set; } = string.Empty;

        
        [Required]
        public Dictionary<string, double> Parametros { get; set; } = new();
    }
}
