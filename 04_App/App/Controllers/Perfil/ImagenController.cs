using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Global;
using Entidad.Dto.Perfil;
using Entidad.Dto.Response.Perfil;
using Entidad.Entidad.Perfil;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Perfil;

namespace App.Controllers.Perfil
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ImagenController : ControllerBase
    {
        private readonly LnImagen _lnImagen = new LnImagen();
        private readonly IMapper mapper;

        public ImagenController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet("ObtenerPorIdAlbumImagen/{id}")]
        public async Task<ActionResult<ImagenResponseObtenerDto>> Obtener(long id)
        {
            ImagenResponseObtenerDto respuesta = new ImagenResponseObtenerDto();
            var result = await Task.FromResult(_lnImagen.ObtenerPorIdAlbumImagen(id));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerImagenPorId")]
        [ProducesResponseType(typeof(ImagenResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ImagenResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<ImagenResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ImagenResponseObtenerPorIdDto respuesta = new ImagenResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnImagen.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ImagenResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(ImagenResponseRegistrarDto), 200)]
        public async Task<ActionResult<ImagenResponseRegistrarDto>> Registrar([FromBody] ImagenRegistrarDto modelo)
        {
            ImagenResponseRegistrarDto respuesta = new ImagenResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnImagen.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ImagenResponseModificarDto), 404)]
        [ProducesResponseType(typeof(ImagenResponseModificarDto), 400)]
        [ProducesResponseType(typeof(ImagenResponseModificarDto), 200)]
        public async Task<ActionResult<ImagenResponseModificarDto>> Modificar([FromBody] Imagen modelo)
        {
            ImagenResponseModificarDto respuesta = new ImagenResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnImagen.ObtenerPorId(modelo.IdImagen));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnImagen.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ImagenResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(ImagenResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(ImagenResponseEliminarDto), 200)]
        public async Task<ActionResult<ImagenResponseEliminarDto>> Eliminar(int id)
        {
            ImagenResponseEliminarDto respuesta = new ImagenResponseEliminarDto();
            var entidad = await Task.FromResult(_lnImagen.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnImagen.Eliminar(id));
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
