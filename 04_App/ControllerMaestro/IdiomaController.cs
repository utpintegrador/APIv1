using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Global;
using Entidad.Dto.Maestro;
using Entidad.Dto.Response.Maestro;
using Entidad.Entidad.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IdiomaController : ControllerBase
    {
        private readonly LnIdioma _lnIdioma = new LnIdioma();
        private readonly IMapper mapper;

        public IdiomaController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IdiomaResponseObtenerDto>> Obtener()
        {
            IdiomaResponseObtenerDto respuesta = new IdiomaResponseObtenerDto();
            var result = await Task.FromResult(_lnIdioma.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerIdiomaPorId")]
        [ProducesResponseType(typeof(IdiomaResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(IdiomaResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<IdiomaResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            IdiomaResponseObtenerPorIdDto respuesta = new IdiomaResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnIdioma.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(IdiomaResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(IdiomaResponseRegistrarDto), 200)]
        public async Task<ActionResult<IdiomaResponseRegistrarDto>> Registrar([FromBody] IdiomaRegistrarDto modelo)
        {
            IdiomaResponseRegistrarDto respuesta = new IdiomaResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnIdioma.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(IdiomaResponseModificarDto), 404)]
        [ProducesResponseType(typeof(IdiomaResponseModificarDto), 400)]
        [ProducesResponseType(typeof(IdiomaResponseModificarDto), 200)]
        public async Task<ActionResult<IdiomaResponseModificarDto>> Modificar([FromBody] Idioma modelo)
        {
            IdiomaResponseModificarDto respuesta = new IdiomaResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnIdioma.ObtenerPorId(modelo.IdIdioma));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnIdioma.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IdiomaResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(IdiomaResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(IdiomaResponseEliminarDto), 200)]
        public async Task<ActionResult<IdiomaResponseEliminarDto>> Eliminar(int id)
        {
            IdiomaResponseEliminarDto respuesta = new IdiomaResponseEliminarDto();
            var entidad = await Task.FromResult(_lnIdioma.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnIdioma.Eliminar(id));
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
