using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Maestro;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoIdentificacionController : ControllerBase
    {
        private readonly LnTipoDocumentoIdentificacion _lnTipoDocumentoIdentificacion = new LnTipoDocumentoIdentificacion();
        private readonly IMapper mapper;

        public TipoDocumentoIdentificacionController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionObtenerDto>> Obtener(RequestTipoDocumentoIdentificacionObtenerDto filtro)
        {
            ResponseTipoDocumentoIdentificacionObtenerDto respuesta = new ResponseTipoDocumentoIdentificacionObtenerDto();
            var result = await Task.FromResult(_lnTipoDocumentoIdentificacion.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionObtenerComboDto>> ObtenerCombo()
        {
            ResponseTipoDocumentoIdentificacionObtenerComboDto respuesta = new ResponseTipoDocumentoIdentificacionObtenerComboDto();

            var result = await Task.FromResult(_lnTipoDocumentoIdentificacion.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoDocumentoIdentificacionPorId")]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseTipoDocumentoIdentificacionObtenerPorIdDto respuesta = new ResponseTipoDocumentoIdentificacionObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnTipoDocumentoIdentificacion.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionRegistrarDto>> Registrar([FromBody] RequestTipoDocumentoIdentificacionRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoDocumentoIdentificacionRegistrarDto respuesta = new ResponseTipoDocumentoIdentificacionRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoDocumentoIdentificacion.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionModificarDto>> Modificar([FromBody] RequestTipoDocumentoIdentificacionModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoDocumentoIdentificacionModificarDto respuesta = new ResponseTipoDocumentoIdentificacionModificarDto();

            var entidad = await Task.FromResult(_lnTipoDocumentoIdentificacion.ObtenerPorId(modelo.IdTipoDocumentoIdentificacion));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoDocumentoIdentificacion.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDocumentoIdentificacionEliminarDto), 200)]
        public async Task<ActionResult<ResponseTipoDocumentoIdentificacionEliminarDto>> Eliminar(int id)
        {
            ResponseTipoDocumentoIdentificacionEliminarDto respuesta = new ResponseTipoDocumentoIdentificacionEliminarDto();
            var entidad = await Task.FromResult(_lnTipoDocumentoIdentificacion.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoDocumentoIdentificacion.Eliminar(id));
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