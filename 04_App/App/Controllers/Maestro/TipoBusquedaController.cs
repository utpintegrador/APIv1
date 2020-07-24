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
    public class TipoBusquedaController : ControllerBase
    {
        private readonly LnTipoBusqueda _lnTipoBusqueda = new LnTipoBusqueda();
        private readonly IMapper mapper;

        public TipoBusquedaController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseTipoBusquedaObtenerDto>> Obtener(RequestTipoBusquedaObtenerDto filtro)
        {
            ResponseTipoBusquedaObtenerDto respuesta = new ResponseTipoBusquedaObtenerDto();
            var result = await Task.FromResult(_lnTipoBusqueda.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseTipoBusquedaObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseTipoBusquedaObtenerComboDto>> ObtenerCombo()
        {
            ResponseTipoBusquedaObtenerComboDto respuesta = new ResponseTipoBusquedaObtenerComboDto();

            var result = await Task.FromResult(_lnTipoBusqueda.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoBusquedaPorId")]
        [ProducesResponseType(typeof(ResponseTipoBusquedaObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseTipoBusquedaObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseTipoBusquedaObtenerPorIdDto respuesta = new ResponseTipoBusquedaObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnTipoBusqueda.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseTipoBusquedaRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoBusquedaRegistrarDto>> Registrar([FromBody] RequestTipoBusquedaRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoBusquedaRegistrarDto respuesta = new ResponseTipoBusquedaRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoBusqueda.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseTipoBusquedaModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseTipoBusquedaModificarDto>> Modificar([FromBody] RequestTipoBusquedaModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseTipoBusquedaModificarDto respuesta = new ResponseTipoBusquedaModificarDto();

            var entidad = await Task.FromResult(_lnTipoBusqueda.ObtenerPorId(modelo.IdTipoBusqueda));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoBusqueda.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseTipoBusquedaEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseTipoBusquedaEliminarDto), 200)]
        public async Task<ActionResult<ResponseTipoBusquedaEliminarDto>> Eliminar(int id)
        {
            ResponseTipoBusquedaEliminarDto respuesta = new ResponseTipoBusquedaEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnTipoBusqueda.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoBusqueda.Eliminar(id));
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