using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
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
        public async Task<ActionResult<MonedaResponseObtenerDto>> Obtener()
        {
            MonedaResponseObtenerDto respuesta = new MonedaResponseObtenerDto();
            var result = await Task.FromResult(_lnMoneda.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerMonedaPorId")]
        [ProducesResponseType(typeof(MonedaResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(MonedaResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<MonedaResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            MonedaResponseObtenerPorIdDto respuesta = new MonedaResponseObtenerPorIdDto();
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
        [ProducesResponseType(typeof(MonedaResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(MonedaResponseRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<MonedaResponseRegistrarDto>> Registrar([FromBody] MonedaRegistrarPrmDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            MonedaResponseRegistrarDto respuesta = new MonedaResponseRegistrarDto();
            
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
        [ProducesResponseType(typeof(MonedaResponseModificarDto), 404)]
        [ProducesResponseType(typeof(MonedaResponseModificarDto), 400)]
        [ProducesResponseType(typeof(MonedaResponseModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<MonedaResponseModificarDto>> Modificar([FromBody] MonedaModificarPrmDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            MonedaResponseModificarDto respuesta = new MonedaResponseModificarDto();
            
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
        [ProducesResponseType(typeof(MonedaResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(MonedaResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(MonedaResponseEliminarDto), 200)]
        public async Task<ActionResult<MonedaResponseEliminarDto>> Eliminar(int id)
        {
            MonedaResponseEliminarDto respuesta = new MonedaResponseEliminarDto();
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
    }
}