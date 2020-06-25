using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Dto.Perfil;
using Entidad.Response.Perfil;
using Entidad.Entidad.Perfil;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Perfil;

namespace App.Controllers.Perfil
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AlbumImagenController : ControllerBase
    {
        private readonly LnAlbumImagen _lnAlbumImagen = new LnAlbumImagen();
        private readonly IMapper mapper;

        public AlbumImagenController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet("ObtenerPorIdPerfil/{id}")]
        public async Task<ActionResult<AlbumImagenResponseObtenerDto>> Obtener(long id)
        {
            AlbumImagenResponseObtenerDto respuesta = new AlbumImagenResponseObtenerDto();
            var result = await Task.FromResult(_lnAlbumImagen.ObtenerPorIdPerfil(id));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerAlbumImagenPorId")]
        [ProducesResponseType(typeof(AlbumImagenResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(AlbumImagenResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<AlbumImagenResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            AlbumImagenResponseObtenerPorIdDto respuesta = new AlbumImagenResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnAlbumImagen.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(AlbumImagenResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(AlbumImagenResponseRegistrarDto), 200)]
        public async Task<ActionResult<AlbumImagenResponseRegistrarDto>> Registrar([FromBody] AlbumImagenRegistrarDto modelo)
        {
            AlbumImagenResponseRegistrarDto respuesta = new AlbumImagenResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnAlbumImagen.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(AlbumImagenResponseModificarDto), 404)]
        [ProducesResponseType(typeof(AlbumImagenResponseModificarDto), 400)]
        [ProducesResponseType(typeof(AlbumImagenResponseModificarDto), 200)]
        public async Task<ActionResult<AlbumImagenResponseModificarDto>> Modificar([FromBody] AlbumImagen modelo)
        {
            AlbumImagenResponseModificarDto respuesta = new AlbumImagenResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnAlbumImagen.ObtenerPorId(modelo.IdAlbumImagen));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnAlbumImagen.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AlbumImagenResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(AlbumImagenResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(AlbumImagenResponseEliminarDto), 200)]
        public async Task<ActionResult<AlbumImagenResponseEliminarDto>> Eliminar(int id)
        {
            AlbumImagenResponseEliminarDto respuesta = new AlbumImagenResponseEliminarDto();
            var entidad = await Task.FromResult(_lnAlbumImagen.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnAlbumImagen.Eliminar(id));
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
