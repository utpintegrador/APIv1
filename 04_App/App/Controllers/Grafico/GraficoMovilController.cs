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
    public class GraficoMovilController : ControllerBase
    {
        private readonly LnGraficoMovil _lnGraficoMovil = new LnGraficoMovil();
        private readonly IMapper mapper;

        public GraficoMovilController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerResumenCompras")]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenComprasDto), 404)]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenComprasDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseGraficoObtenerResumenComprasDto>> ObtenerResumenCompras([FromBody] RequestGraficoObtenerResumenComprasDto prm)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseGraficoObtenerResumenComprasDto respuesta = new ResponseGraficoObtenerResumenComprasDto();

            var entidad = await Task.FromResult(_lnGraficoMovil.ObtenerResumenCompras(prm));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpPost("ObtenerResumenVentas")]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenVentasDto), 404)]
        [ProducesResponseType(typeof(ResponseGraficoObtenerResumenVentasDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseGraficoObtenerResumenVentasDto>> ObtenerResumenVentas([FromBody] RequestGraficoObtenerResumenVentasDto prm)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseGraficoObtenerResumenVentasDto respuesta = new ResponseGraficoObtenerResumenVentasDto();

            var entidad = await Task.FromResult(_lnGraficoMovil.ObtenerResumenVentas(prm));
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