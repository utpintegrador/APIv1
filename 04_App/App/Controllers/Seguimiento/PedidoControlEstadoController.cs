using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Seguimiento;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguimiento;

namespace App.Controllers.Seguimiento
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoControlEstadoController : ControllerBase
    {
        private readonly LnPedidoControlEstado _lnPedidoControlEstado = new LnPedidoControlEstado();
        private readonly IMapper _mapper;

        public PedidoControlEstadoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("ObtenerPorIdPedido/{id}")]
        [ProducesResponseType(typeof(ResponsePedidoControlEstadoObtenerPorIdPedidoDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoControlEstadoObtenerPorIdPedidoDto), 200)]
        public async Task<ActionResult<ResponsePedidoControlEstadoObtenerPorIdPedidoDto>> ObtenerPorIdPedido(long id)
        {
            ResponsePedidoControlEstadoObtenerPorIdPedidoDto respuesta = new ResponsePedidoControlEstadoObtenerPorIdPedidoDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnPedidoControlEstado.ObtenerPorIdPedido(id));
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