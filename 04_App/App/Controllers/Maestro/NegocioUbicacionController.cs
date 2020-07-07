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
    public class NegocioUbicacionController : ControllerBase
    {
        private readonly LnNegocioUbicacion _lnNegocioUbicacion = new LnNegocioUbicacion();
        private readonly IMapper mapper;

        public NegocioUbicacionController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerPorIdNegocio")]
        public async Task<ActionResult<ResponseNegocioUbicacionObtenerPorIdNegocioDto>> ObtenerPorIdNegocio([FromBody] RequestNegocioUbicacionObtenerPorIdNegocioDto filtro)
        {
            ResponseNegocioUbicacionObtenerPorIdNegocioDto respuesta = new ResponseNegocioUbicacionObtenerPorIdNegocioDto();
            var result = await Task.FromResult(_lnNegocioUbicacion.ObtenerPorIdNegocio(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerNegocioUbicacionPorId")]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseNegocioUbicacionObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponseNegocioUbicacionObtenerPorIdDto respuesta = new ResponseNegocioUbicacionObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnNegocioUbicacion.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseNegocioUbicacionRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseNegocioUbicacionRegistrarDto>> Registrar([FromBody] RequestNegocioUbicacionRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseNegocioUbicacionRegistrarDto respuesta = new ResponseNegocioUbicacionRegistrarDto();

            long nuevoId = 0;
            var result = await Task.FromResult(_lnNegocioUbicacion.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        /// <summary>
        /// IdEstado: 1 es Activo  |   2 es Inactivo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseNegocioUbicacionModificarDto>> Modificar([FromBody] RequestNegocioUbicacionModificarDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            ResponseNegocioUbicacionModificarDto respuesta = new ResponseNegocioUbicacionModificarDto();

            var entidad = await Task.FromResult(_lnNegocioUbicacion.ObtenerPorId(modelo.IdNegocioUbicacion));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnNegocioUbicacion.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioUbicacionEliminarDto), 200)]
        public async Task<ActionResult<ResponseNegocioUbicacionEliminarDto>> Eliminar(int id)
        {
            ResponseNegocioUbicacionEliminarDto respuesta = new ResponseNegocioUbicacionEliminarDto();
            var entidad = await Task.FromResult(_lnNegocioUbicacion.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnNegocioUbicacion.Eliminar(id));
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