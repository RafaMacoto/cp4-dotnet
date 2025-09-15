using System.ComponentModel.DataAnnotations;

namespace cp4_dotnet.DTOs
{
    public class ValidacaoContencaoRequest
    {
        [Required]
        public FormaRequest FormaExterna { get; set; }

        [Required]
        public FormaRequest FormaInterna { get; set; }
    }
}