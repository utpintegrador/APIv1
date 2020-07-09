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
    public class TipoDescuentoController : ControllerBase
    {
        private readonly LnTipoDescuento _lnTipoDescuento = new LnTipoDescuento();
        private readonly IMapper mapper;

        public TipoDescuentoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseTipoDescuentoObtenerDto>> Obtener(RequestTipoDescuentoObtenerDto filtro)
        {
            ResponseTipoDescuentoObtenerDto respuesta = new ResponseTipoDescuentoObtenerDto();
            var result = await Task.FromResult(_lnTipoDescuento.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseTipoDescuentoObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseTipoDescuentoObtenerComboDto>> ObtenerCombo()
        {
            ResponseTipoDescuentoObtenerComboDto respuesta = new ResponseTipoDescuentoObtenerComboDto();

            var result = await Task.FromResult(_lnTipoDescuento.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoDescuentoPorId")]
        [ProducesResponseType(typeof(ResponseTipoDescuentoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseTipoDescuentoObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseTipoDescuentoObtenerPorIdDto respuesta = new ResponseTipoDescuentoObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnTipoDescuento.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseTipoDescuentoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoDescuentoRegistrarDto>> Registrar([FromBody] RequestTipoDescuentoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoDescuentoRegistrarDto respuesta = new ResponseTipoDescuentoRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoDescuento.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseTipoDescuentoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoDescuentoModificarDto>> Modificar([FromBody] RequestTipoDescuentoModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoDescuentoModificarDto respuesta = new ResponseTipoDescuentoModificarDto();

            var entidad = await Task.FromResult(_lnTipoDescuento.ObtenerPorId(modelo.IdTipoDescuento));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoDescuento.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseTipoDescuentoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoDescuentoEliminarDto), 200)]
        public async Task<ActionResult<ResponseTipoDescuentoEliminarDto>> Eliminar(int id)
        {
            ResponseTipoDescuentoEliminarDto respuesta = new ResponseTipoDescuentoEliminarDto();
            var entidad = await Task.FromResult(_lnTipoDescuento.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoDescuento.Eliminar(id));
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