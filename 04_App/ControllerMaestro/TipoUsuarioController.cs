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
    public class TipoUsuarioController : ControllerBase
    {
        private readonly LnTipoUsuario _lnTipoUsuario = new LnTipoUsuario();
        private readonly IMapper mapper;

        public TipoUsuarioController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TipoUsuarioResponseObtenerDto>> Obtener()
        {
            TipoUsuarioResponseObtenerDto respuesta = new TipoUsuarioResponseObtenerDto();
            var result = await Task.FromResult(_lnTipoUsuario.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoUsuarioPorId")]
        [ProducesResponseType(typeof(TipoUsuarioResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(TipoUsuarioResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<TipoUsuarioResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            TipoUsuarioResponseObtenerPorIdDto respuesta = new TipoUsuarioResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnTipoUsuario.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(TipoUsuarioResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(TipoUsuarioResponseRegistrarDto), 200)]
        public async Task<ActionResult<TipoUsuarioResponseRegistrarDto>> Registrar([FromBody] TipoUsuarioRegistrarDto modelo)
        {
            TipoUsuarioResponseRegistrarDto respuesta = new TipoUsuarioResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoUsuario.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(TipoUsuarioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(TipoUsuarioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(TipoUsuarioResponseModificarDto), 200)]
        public async Task<ActionResult<TipoUsuarioResponseModificarDto>> Modificar([FromBody] TipoUsuario modelo)
        {
            TipoUsuarioResponseModificarDto respuesta = new TipoUsuarioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnTipoUsuario.ObtenerPorId(modelo.IdTipoUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoUsuario.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TipoUsuarioResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(TipoUsuarioResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(TipoUsuarioResponseEliminarDto), 200)]
        public async Task<ActionResult<TipoUsuarioResponseEliminarDto>> Eliminar(int id)
        {
            TipoUsuarioResponseEliminarDto respuesta = new TipoUsuarioResponseEliminarDto();
            var entidad = await Task.FromResult(_lnTipoUsuario.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoUsuario.Eliminar(id));
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
