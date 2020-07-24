using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Grafico;
using Entidad.Response;
using Entidad.Response.Grafico;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Grafico;

namespace App.Controllers.Grafico
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraficoWebController : ControllerBase
    {
        private readonly LnGraficoWeb _lnGraficoWeb = new LnGraficoWeb();
        private readonly IMapper mapper;

        public GraficoWebController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerResumenWeb")]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenWebDto), 404)]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenWebDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseGraficoObtenerResumenWebDto>> ObtenerResumenWeb([FromBody] RequestGraficoObtenerResumenDto prm)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseGraficoObtenerResumenWebDto respuesta = new ResponseGraficoObtenerResumenWebDto();

            var entidad = await Task.FromResult(_lnGraficoWeb.ObtenerResumenWeb(prm));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }
    }
}