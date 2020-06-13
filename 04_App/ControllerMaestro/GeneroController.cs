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
    public class GeneroController : ControllerBase
    {
        private readonly LnGenero _lnGenero = new LnGenero();
        private readonly IMapper mapper;

        public GeneroController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneroResponseObtenerDto>> Obtener()
        {
            GeneroResponseObtenerDto respuesta = new GeneroResponseObtenerDto();
            var result = await Task.FromResult(_lnGenero.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerGeneroPorId")]
        [ProducesResponseType(typeof(GeneroResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(GeneroResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<GeneroResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            GeneroResponseObtenerPorIdDto respuesta = new GeneroResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnGenero.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(GeneroResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(GeneroResponseRegistrarDto), 200)]
        public async Task<ActionResult<GeneroResponseRegistrarDto>> Registrar([FromBody] GeneroRegistrarDto modelo)
        {
            GeneroResponseRegistrarDto respuesta = new GeneroResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnGenero.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(GeneroResponseModificarDto), 404)]
        [ProducesResponseType(typeof(GeneroResponseModificarDto), 400)]
        [ProducesResponseType(typeof(GeneroResponseModificarDto), 200)]
        public async Task<ActionResult<GeneroResponseModificarDto>> Modificar([FromBody] Genero modelo)
        {
            GeneroResponseModificarDto respuesta = new GeneroResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnGenero.ObtenerPorId(modelo.IdGenero));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnGenero.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GeneroResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(GeneroResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(GeneroResponseEliminarDto), 200)]
        public async Task<ActionResult<GeneroResponseEliminarDto>> Eliminar(int id)
        {
            GeneroResponseEliminarDto respuesta = new GeneroResponseEliminarDto();
            var entidad = await Task.FromResult(_lnGenero.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnGenero.Eliminar(id));
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
