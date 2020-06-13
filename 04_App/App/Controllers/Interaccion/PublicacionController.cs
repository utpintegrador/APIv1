using System.Threading.Tasks;
using Entidad.Dto.Global;
using Entidad.Dto.Interaccion;
using Entidad.Dto.Response.Interaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Interaccion;

namespace App.Controllers.Interaccion
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PublicacionController : ControllerBase
    {
        private readonly LnPublicacion _lnPublicacion = new LnPublicacion();
        [HttpGet("ObtenerPorIdUsuario/{id}")]
        public async Task<ActionResult<PublicacionResponseObtenerPorIdUsuarioDto>> ObtenerPorIdUsuario(long id)
        {
            PublicacionResponseObtenerPorIdUsuarioDto respuesta = new PublicacionResponseObtenerPorIdUsuarioDto();
            var result = await Task.FromResult(_lnPublicacion.ObtenerPorIdUsuario(id));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PublicacionResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(PublicacionResponseRegistrarDto), 200)]
        public async Task<ActionResult<PublicacionResponseRegistrarDto>> Registrar([FromBody] PublicacionRegistrarDto modelo)
        {
            PublicacionResponseRegistrarDto respuesta = new PublicacionResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnPublicacion.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            var registroCreado = await Task.FromResult(_lnPublicacion.ObtenerPorId(nuevoId));
            if(registroCreado != null)
            {
                respuesta.Cuerpo = registroCreado;
            }
            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PublicacionResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(PublicacionResponseEliminarDto), 200)]
        public async Task<ActionResult<PublicacionResponseEliminarDto>> Eliminar(long id)
        {
            PublicacionResponseEliminarDto respuesta = new PublicacionResponseEliminarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnPublicacion.Eliminar(id));
            if(result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdRespuesta = 1;
            return Ok(respuesta);
        }

    }
}
