using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Global;
using Entidad.Dto.Maestro;
using Entidad.Dto.Response.Maestro;
using Entidad.Entidad.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CarreraController : ControllerBase
    {
        private readonly LnCarrera _lnCarrera = new LnCarrera();
        private readonly IMapper mapper;

        public CarreraController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CarreraResponseObtenerDto>> Obtener()
        {
            CarreraResponseObtenerDto respuesta = new CarreraResponseObtenerDto();
            var result = await Task.FromResult(_lnCarrera.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerCarreraPorId")]
        [ProducesResponseType(typeof(CarreraResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(CarreraResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<CarreraResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            CarreraResponseObtenerPorIdDto respuesta = new CarreraResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnCarrera.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(CarreraResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(CarreraResponseRegistrarDto), 200)]
        public async Task<ActionResult<CarreraResponseRegistrarDto>> Registrar([FromBody] CarreraRegistrarDto modelo)
        {
            CarreraResponseRegistrarDto respuesta = new CarreraResponseRegistrarDto();
            if (!ModelState.IsValid) {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnCarrera.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(CarreraResponseModificarDto), 404)]
        [ProducesResponseType(typeof(CarreraResponseModificarDto), 400)]
        [ProducesResponseType(typeof(CarreraResponseModificarDto), 200)]
        public async Task<ActionResult<CarreraResponseModificarDto>> Modificar([FromBody] Carrera modelo)
        {
            CarreraResponseModificarDto respuesta = new CarreraResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnCarrera.ObtenerPorId(modelo.IdCarrera));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnCarrera.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CarreraResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(CarreraResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(CarreraResponseEliminarDto), 200)]
        public async Task<ActionResult<CarreraResponseEliminarDto>> Eliminar(int id)
        {
            CarreraResponseEliminarDto respuesta = new CarreraResponseEliminarDto();
            var entidad = await Task.FromResult(_lnCarrera.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnCarrera.Eliminar(id));
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
