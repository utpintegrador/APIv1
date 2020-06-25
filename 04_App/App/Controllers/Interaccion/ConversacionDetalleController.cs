using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Dto.Interaccion;
using Entidad.Response.Interaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Interaccion;

namespace App.Controllers.Interaccion
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ConversacionDetalleController : ControllerBase
    {
        private readonly LnConversacionDetalle _lnConversacionDetalle = new LnConversacionDetalle();
        private readonly IMapper _mapper;

        public ConversacionDetalleController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet("ObtenerPorIdConversacion/{id}")]
        [ProducesResponseType(typeof(ConversacionDetalleResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ConversacionDetalleResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<ConversacionDetalleResponseObtenerPorIdDto>> ObtenerPorIdConversacion(long id)
        {
            ConversacionDetalleResponseObtenerPorIdDto respuesta = new ConversacionDetalleResponseObtenerPorIdDto();
            var listado = await Task.FromResult(_lnConversacionDetalle.ObtenerPorIdConversacion(id));
            if (listado == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = listado;
            return Ok(respuesta);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ConversacionDetalleResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(ConversacionDetalleResponseRegistrarDto), 200)]
        public async Task<ActionResult<ConversacionDetalleResponseRegistrarDto>> Registrar([FromBody] ConversacionDetalleRegistrarDto modelo)
        {
            ConversacionDetalleResponseRegistrarDto respuesta = new ConversacionDetalleResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnConversacionDetalle.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [HttpPost("ObtenerMensajesNuevos")]
        [ProducesResponseType(typeof(ConversacionDetalleResponseObtenerMensajeNuevoDto), 400)]
        [ProducesResponseType(typeof(ConversacionDetalleResponseObtenerMensajeNuevoDto), 200)]
        public async Task<ActionResult<ConversacionDetalleResponseObtenerMensajeNuevoDto>> ObtenerMensajesNuevos([FromBody] ConversacionDetalleFiltroObtenerDto modelo)
        {
            ConversacionDetalleResponseObtenerMensajeNuevoDto respuesta = new ConversacionDetalleResponseObtenerMensajeNuevoDto();

            var result = await Task.FromResult(_lnConversacionDetalle.ObtenerMensajesNuevos(modelo.IdUsuario, modelo.IdConversacion));
            if (result == null)
            {
                result = new List<ConversacionDetalleObtenerMensajeNuevoDto>();
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            return Ok(respuesta);

        }

    }
}
