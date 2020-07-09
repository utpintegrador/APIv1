using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;
using App.CustomHandler;
using Entidad.Request.Maestro;
using System.Linq;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly LnTipoUsuario _lnTipoUsuario = new LnTipoUsuario();
        private readonly IMapper mapper;

        public TipoUsuarioController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseTipoUsuarioObtenerDto>> Obtener(RequestTipoUsuarioObtenerDto filtro)
        {
            ResponseTipoUsuarioObtenerDto respuesta = new ResponseTipoUsuarioObtenerDto();
            var result = await Task.FromResult(_lnTipoUsuario.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoUsuarioPorId")]
        [ProducesResponseType(typeof(ResponseTipoUsuarioObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseTipoUsuarioObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseTipoUsuarioObtenerPorIdDto respuesta = new ResponseTipoUsuarioObtenerPorIdDto();
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
        [ProducesResponseType(typeof(ResponseTipoUsuarioRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoUsuarioRegistrarDto>> Registrar([FromBody] RequestTipoUsuarioRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoUsuarioRegistrarDto respuesta = new ResponseTipoUsuarioRegistrarDto();

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
        [ProducesResponseType(typeof(ResponseTipoUsuarioModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoUsuarioModificarDto>> Modificar([FromBody] RequestTipoUsuarioModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoUsuarioModificarDto respuesta = new ResponseTipoUsuarioModificarDto();

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
        [ProducesResponseType(typeof(ResponseTipoUsuarioEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioEliminarDto), 200)]
        public async Task<ActionResult<ResponseTipoUsuarioEliminarDto>> Eliminar(int id)
        {
            ResponseTipoUsuarioEliminarDto respuesta = new ResponseTipoUsuarioEliminarDto();
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

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseTipoUsuarioObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioObtenerComboDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoUsuarioObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseTipoUsuarioObtenerComboDto>> ObtenerCombo()
        {
            ResponseTipoUsuarioObtenerComboDto respuesta = new ResponseTipoUsuarioObtenerComboDto();

            var result = await Task.FromResult(_lnTipoUsuario.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}