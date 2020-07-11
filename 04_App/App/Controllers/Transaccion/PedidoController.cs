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

        /// <summary>
        /// Accion: read, add, upd, del
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoModificarDto>> Modificar([FromBody] RequestPedidoModificarDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoModificarDto respuesta = new ResponsePedidoModificarDto();

            bool validacionItems = false;
            var listaValidada = CustomValidation.ValidacionProductoDetalle.ValidarListaModificar(modelo, ref validacionItems);
            if (!validacionItems)
            {
                respuesta.ListaError.AddRange(listaValidada);
                return BadRequest(respuesta);
            }

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

        /// <summary>
        /// 7   Generado
        /// 12  Cancelado
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut("ModificarEstadoPorParteDeComprador")]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeCompradorDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeCompradorDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeCompradorDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoModificarEstadoPorParteDeCompradorDto>> ModificarEstadoPorParteDeComprador([FromBody] RequestPedidoModificarEstadoPorParteDeCompradorDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoModificarEstadoPorParteDeCompradorDto respuesta = new ResponsePedidoModificarEstadoPorParteDeCompradorDto();
            //7 12
            if(modelo.IdEstado != 7 && modelo.IdEstado != 12)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "IdEstado proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPedido.ObtenerPorId(modelo.IdPedido));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPedido.ModificarEstadoPorParteDeComprador(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        /// <summary>
        /// 8     Aceptado
        /// 9     Preparando
        /// 10    Entregando
        /// 11    Entregado
        /// 13    Rechazado
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut("ModificarEstadoPorParteDeVendedor")]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeVendedorDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeVendedorDto), 400)]
        [ProducesResponseType(typeof(ResponsePedidoModificarEstadoPorParteDeVendedorDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponsePedidoModificarEstadoPorParteDeVendedorDto>> ModificarEstadoPorParteDeVendedor([FromBody] RequestPedidoModificarEstadoPorParteDeVendedorDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponsePedidoModificarEstadoPorParteDeVendedorDto respuesta = new ResponsePedidoModificarEstadoPorParteDeVendedorDto();
            //8     Aceptado    3 *
            //9     Preparando  3 *
            //10    Entregando  3 *
            //11    Entregado   3 *
            //13    Rechazado   3 *
            if (modelo.IdEstado != 8 && modelo.IdEstado != 9 && modelo.IdEstado != 10 && modelo.IdEstado != 11 && modelo.IdEstado != 13)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "IdEstado proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPedido.ObtenerPorId(modelo.IdPedido));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPedido.ModificarEstadoPorParteDeVendedor(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerPorIdConDetalles/{id}")]
        [ProducesResponseType(typeof(ResponsePedidoObtenerPorIdConDetallesDto), 404)]
        [ProducesResponseType(typeof(ResponsePedidoObtenerPorIdConDetallesDto), 200)]
        public async Task<ActionResult<ResponsePedidoObtenerPorIdConDetallesDto>> ObtenerPorIdConDetalles(long id)
        {
            ResponsePedidoObtenerPorIdConDetallesDto respuesta = new ResponsePedidoObtenerPorIdConDetallesDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "id: parametro requerido" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnPedido.ObtenerPorIdConDetalles(id));
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