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
    public class InteresGeneroController : ControllerBase
    {
        private readonly LnInteresGenero _lnInteresGenero = new LnInteresGenero();
        private readonly IMapper mapper;

        public InteresGeneroController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<InteresGeneroResponseObtenerDto>> Obtener()
        {
            InteresGeneroResponseObtenerDto respuesta = new InteresGeneroResponseObtenerDto();
            var result = await Task.FromResult(_lnInteresGenero.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerInteresGeneroPorId")]
        [ProducesResponseType(typeof(InteresGeneroResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(InteresGeneroResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<InteresGeneroResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            InteresGeneroResponseObtenerPorIdDto respuesta = new InteresGeneroResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnInteresGenero.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(InteresGeneroResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(InteresGeneroResponseRegistrarDto), 200)]
        public async Task<ActionResult<InteresGeneroResponseRegistrarDto>> Registrar([FromBody] InteresGeneroRegistrarDto modelo)
        {
            InteresGeneroResponseRegistrarDto respuesta = new InteresGeneroResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnInteresGenero.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(InteresGeneroResponseModificarDto), 404)]
        [ProducesResponseType(typeof(InteresGeneroResponseModificarDto), 400)]
        [ProducesResponseType(typeof(InteresGeneroResponseModificarDto), 200)]
        public async Task<ActionResult<InteresGeneroResponseModificarDto>> Modificar([FromBody] InteresGenero modelo)
        {
            InteresGeneroResponseModificarDto respuesta = new InteresGeneroResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnInteresGenero.ObtenerPorId(modelo.IdInteresGenero));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnInteresGenero.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(InteresGeneroResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(InteresGeneroResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(InteresGeneroResponseEliminarDto), 200)]
        public async Task<ActionResult<InteresGeneroResponseEliminarDto>> Eliminar(int id)
        {
            InteresGeneroResponseEliminarDto respuesta = new InteresGeneroResponseEliminarDto();
            var entidad = await Task.FromResult(_lnInteresGenero.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnInteresGenero.Eliminar(id));
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