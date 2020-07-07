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
    public class MonedaController : ControllerBase
    {
        private readonly LnMoneda _lnMoneda = new LnMoneda();
        private readonly IMapper mapper;

        public MonedaController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseMonedaObtenerDto>> Obtener()
        {
            ResponseMonedaObtenerDto respuesta = new ResponseMonedaObtenerDto();
            var result = await Task.FromResult(_lnMoneda.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerMonedaPorId")]
        [ProducesResponseType(typeof(ResponseMonedaObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseMonedaObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseMonedaObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseMonedaObtenerPorIdDto respuesta = new ResponseMonedaObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnMoneda.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseMonedaRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseMonedaRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseMonedaRegistrarDto>> Registrar([FromBody] RequestMonedaRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseMonedaRegistrarDto respuesta = new ResponseMonedaRegistrarDto();
            
            int nuevoId = 0;
            var result = await Task.FromResult(_lnMoneda.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseMonedaModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseMonedaModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseMonedaModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseMonedaModificarDto>> Modificar([FromBody] RequestMonedaModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseMonedaModificarDto respuesta = new ResponseMonedaModificarDto();
            
            var entidad = await Task.FromResult(_lnMoneda.ObtenerPorId(modelo.IdMoneda));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnMoneda.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseMonedaEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseMonedaEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseMonedaEliminarDto), 200)]
        public async Task<ActionResult<ResponseMonedaEliminarDto>> Eliminar(int id)
        {
            ResponseMonedaEliminarDto respuesta = new ResponseMonedaEliminarDto();
            var entidad = await Task.FromResult(_lnMoneda.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnMoneda.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseMonedaObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseMonedaObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseMonedaObtenerComboDto>> ObtenerCombo()
        {
            ResponseMonedaObtenerComboDto respuesta = new ResponseMonedaObtenerComboDto();

            var result = await Task.FromResult(_lnMoneda.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}