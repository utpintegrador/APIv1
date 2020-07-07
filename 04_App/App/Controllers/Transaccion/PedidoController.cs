using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Transaccion;
using Entidad.Response;
using Entidad.Response.Transaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Transaccion;

namespace App.Controllers.Transaccion
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly LnPedido _lnPedido = new LnPedido();
        private readonly IMapper mapper;

        public PedidoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerPorIdNegocioComprador")]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoObtenerPorIdNegocioCompradorDto>> ObtenerPorIdNegocioComprador([FromBody] RequestPedidoObtenerPorIdNegocioCompradorDto filtro)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoObtenerPorIdNegocioCompradorDto respuesta = new ResponsePedidoObtenerPorIdNegocioCompradorDto();
            var result = await Task.FromResult(_lnPedido.ObtenerPorIdNegocioComprador(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpPost("ObtenerPorIdNegocioVendedor")]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoObtenerPorIdNegocioVendedorDto>> ObtenerPorIdNegocioVendedor([FromBody] RequestPedidoObtenerPorIdNegocioVendedorDto filtro)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoObtenerPorIdNegocioVendedorDto respuesta = new ResponsePedidoObtenerPorIdNegocioVendedorDto();
            var result = await Task.FromResult(_lnPedido.ObtenerPorIdNegocioVendedor(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerPedidoPorId")]
        [ProducesResponseType(typeof(ResponsePedidoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponsePedidoObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponsePedidoObtenerPorIdDto respuesta = new ResponsePedidoObtenerPorIdDto();
            if(id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "id: parametro requerido" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnPedido.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponsePedidoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoRegistrarDto>> Registrar([FromBody] RequestPedidoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoRegistrarDto respuesta = new ResponsePedidoRegistrarDto();

            if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<RequestPedidoRegistrarPedidoDetalleRegistrarDto>();
            if (!modelo.ListaPedidoDetalle.Any())
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere como mínimo un detalle" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnPedido.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoModificarDto>> Modificar([FromBody] RequestPedidoModificarDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoModificarDto respuesta = new ResponsePedidoModificarDto();

            var entidad = await Task.FromResult(_lnPedido.ObtenerPorId(modelo.IdPedido));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPedido.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }
    }
}