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
    public class PedidoDetalleController : ControllerBase
    {
        private readonly LnPedidoDetalle _lnPedidoDetalle = new LnPedidoDetalle();
        private readonly IMapper mapper;

        public PedidoDetalleController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //[HttpPost("ObtenerPorIdNegocio")]
        //public async Task<ActionResult<ResponsePedidoDetalleObtenerPorIdNegocioDto>> ObtenerPorIdNegocio([FromBody] RequestPedidoDetalleObtenerPorIdNegocioDto filtro)
        //{
        //    ResponsePedidoDetalleObtenerPorIdNegocioDto respuesta = new ResponsePedidoDetalleObtenerPorIdNegocioDto();
        //    var result = await Task.FromResult(_lnPedidoDetalle.ObtenerPorIdNegocio(filtro));
        //    respuesta.ProcesadoOk = 1;
        //    respuesta.Cuerpo = result;

        //    if (result.Any())
        //    {
        //        respuesta.CantidadTotalRegistros = result.First().TotalItems;
        //    }

        //    return Ok(respuesta);
        //}

        //[HttpGet("{id}", Name = "ObtenerPedidoDetallePorId")]
        //[ProducesResponseType(typeof(ResponsePedidoDetalleObtenerPorIdDto), 404)]
        //[ProducesResponseType(typeof(ResponsePedidoDetalleObtenerPorIdDto), 200)]
        //public async Task<ActionResult<ResponsePedidoDetalleObtenerPorIdDto>> ObtenerPorId(long id)
        //{
        //    ResponsePedidoDetalleObtenerPorIdDto respuesta = new ResponsePedidoDetalleObtenerPorIdDto();
        //    var entidad = await Task.FromResult(_lnPedidoDetalle.ObtenerPorId(id));
        //    if (entidad == null)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
        //        return NotFound(respuesta);
        //    }

        //    respuesta.ProcesadoOk = 1;
        //    respuesta.Cuerpo = entidad;
        //    return Ok(respuesta);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(ResponsePedidoDetalleRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoDetalleRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoDetalleRegistrarDto>> Registrar([FromBody] RequestPedidoDetalleRootRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoDetalleRegistrarDto respuesta = new ResponsePedidoDetalleRegistrarDto();
            if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<RequestPedidoDetalleRegistrarDto>();
            if (!modelo.ListaPedidoDetalle.Any())
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere como mínimo un detalle" });
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnPedidoDetalle.ProcesarRegistro(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;

            return Ok(respuesta);

        }

        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(ResponsePedidoDetalleModificarDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoDetalleModificarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoDetalleModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoDetalleModificarDto>> Modificar([FromBody] RequestPedidoDetalleRootModificarDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoDetalleModificarDto respuesta = new ResponsePedidoDetalleModificarDto();
            if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<RequestPedidoDetalleModificarDto>();
            if (!modelo.ListaPedidoDetalle.Any())
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere como mínimo un detalle" });
                return BadRequest(respuesta);
            }

            //var entidad = await Task.FromResult(_lnPedidoDetalle.ObtenerPorId(modelo.IdPedidoDetalle));
            //if (entidad == null)
            //{
            //    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
            //    return NotFound(respuesta);
            //}

            var result = await Task.FromResult(_lnPedidoDetalle.ProcesarModificacion(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpPost("Eliminar")]
        [ProducesResponseType(typeof(ResponsePedidoDetalleEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoDetalleEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoDetalleEliminarDto), 200)]
        public async Task<ActionResult<ResponsePedidoDetalleEliminarDto>> Eliminar([FromBody] RequestPedidoDetalleRootEliminarDto modelo)
        {
            ResponsePedidoDetalleEliminarDto respuesta = new ResponsePedidoDetalleEliminarDto();
            if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<long>();
            if (!modelo.ListaPedidoDetalle.Any())
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere como mínimo un detalle" });
                return BadRequest(respuesta);
            }
            //var entidad = await Task.FromResult(_lnPedidoDetalle.ObtenerPorId(id));
            //if (entidad == null)
            //{
            //    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
            //    return NotFound(respuesta);
            //}

            int result = await Task.FromResult(_lnPedidoDetalle.ProcesarEliminacion(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }
    }
}