using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;
using App.CustomHandler;
using Entidad.Request.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEstadoController : ControllerBase
    {
        private readonly LnTipoEstado _lnTipoEstado = new LnTipoEstado();
        private readonly IMapper mapper;

        public TipoEstadoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseTipoEstadoObtenerDto>> Obtener()
        {
            ResponseTipoEstadoObtenerDto respuesta = new ResponseTipoEstadoObtenerDto();
            var result = await Task.FromResult(_lnTipoEstado.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoEstadoPorId")]
        [ProducesResponseType(typeof(ResponseTipoEstadoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoEstadoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseTipoEstadoObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseTipoEstadoObtenerPorIdDto respuesta = new ResponseTipoEstadoObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseTipoEstadoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoEstadoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoEstadoRegistrarDto>> Registrar([FromBody] RequestTipoEstadoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoEstadoRegistrarDto respuesta = new ResponseTipoEstadoRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoEstado.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseTipoEstadoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoEstadoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoEstadoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoEstadoModificarDto>> Modificar([FromBody] RequestTipoEstadoModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoEstadoModificarDto respuesta = new ResponseTipoEstadoModificarDto();

            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(modelo.IdTipoEstado));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoEstado.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseTipoEstadoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoEstadoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoEstadoEliminarDto), 200)]
        public async Task<ActionResult<ResponseTipoEstadoEliminarDto>> Eliminar(int id)
        {
            ResponseTipoEstadoEliminarDto respuesta = new ResponseTipoEstadoEliminarDto();
            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoEstado.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseTipoEstadoObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseTipoEstadoObtenerComboDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoEstadoObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseTipoEstadoObtenerComboDto>> ObtenerCombo()
        {
            ResponseTipoEstadoObtenerComboDto respuesta = new ResponseTipoEstadoObtenerComboDto();

            var result = await Task.FromResult(_lnTipoEstado.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}