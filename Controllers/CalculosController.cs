using cp4_dotnet.DTOs;
using cp4_dotnet.Services;
using cp4_dotnet.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cp4_dotnet.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CalculosController : ControllerBase
    {
        private readonly ICalculadoraService _service;
        private readonly IFormaFactory _factory;

        public CalculosController(ICalculadoraService service, IFormaFactory factory)
        {
            _service = service;
            _factory = factory;
        }

        /// <summary>Calcula a área de uma forma 2D (círculo, retângulo).</summary>
        [HttpPost("area")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult CalcularArea([FromBody] FormaRequest dto)
        {
            var forma = _factory.CriarForma(dto);
            var resultado = _service.CalcularArea(forma);
            return Ok(resultado);
        }

        /// <summary>Calcula o perímetro de uma forma 2D.</summary>
        [HttpPost("perimetro")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult CalcularPerimetro([FromBody] FormaRequest dto)
        {
            var forma = _factory.CriarForma(dto);
            var resultado = _service.CalcularPerimetro(forma);
            return Ok(resultado);
        }

        /// <summary>Calcula o volume de uma forma 3D (ex.: esfera).</summary>
        [HttpPost("volume")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult CalcularVolume([FromBody] FormaRequest dto)
        {
            var forma = _factory.CriarForma(dto);
            var resultado = _service.CalcularVolume(forma);
            return Ok(resultado);
        }

        /// <summary>Calcula a área superficial de uma forma 3D (ex.: esfera).</summary>
        [HttpPost("area-superficial")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult CalcularAreaSuperficial([FromBody] FormaRequest dto)
        {
            var forma = _factory.CriarForma(dto);
            var resultado = _service.CalcularAreaSuperficial(forma);
            return Ok(resultado);
        }

        /// <summary>
        /// Verifica se uma forma geométrica pode ser contida dentro de outra.
        /// </summary>
        /// <remarks>
        /// Recebe uma forma externa e uma interna e retorna 'true' se a contenção for possível.
        /// </remarks>

        [HttpPost("validacoes/forma-contida")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult ValidarFormaContida([FromBody] ValidacaoContencaoRequest request)
        {
            try
            {
                var formaExterna = _factory.CriarForma(request.FormaExterna);
                var formaInterna = _factory.CriarForma(request.FormaInterna);

                bool resultado = _service.ValidarContencao(formaExterna, formaInterna);

                return Ok(resultado);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}