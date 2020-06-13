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
    public class GradoAcademicoController : ControllerBase
    {
        private readonly LnGradoAcademico _lnGradoAcademico = new LnGradoAcademico();
        private readonly IMapper mapper;

        public GradoAcademicoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GradoAcademicoResponseObtenerDto>> Obtener()
        {
            GradoAcademicoResponseObtenerDto respuesta = new GradoAcademicoResponseObtenerDto();
            var result = await Task.FromResult(_lnGradoAcademico.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerGradoAcademicoPorId")]
        [ProducesResponseType(typeof(GradoAcademicoResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(GradoAcademicoResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<GradoAcademicoResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            GradoAcademicoResponseObtenerPorIdDto respuesta = new GradoAcademicoResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnGradoAcademico.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(GradoAcademicoResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(GradoAcademicoResponseRegistrarDto), 200)]
        public async Task<ActionResult<GradoAcademicoResponseRegistrarDto>> Registrar([FromBody] GradoAcademicoRegistrarDto modelo)
        {
            GradoAcademicoResponseRegistrarDto respuesta = new GradoAcademicoResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnGradoAcademico.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(GradoAcademicoResponseModificarDto), 404)]
        [ProducesResponseType(typeof(GradoAcademicoResponseModificarDto), 400)]
        [ProducesResponseType(typeof(GradoAcademicoResponseModificarDto), 200)]
        public async Task<ActionResult<GradoAcademicoResponseModificarDto>> Modificar([FromBody] GradoAcademico modelo)
        {
            GradoAcademicoResponseModificarDto respuesta = new GradoAcademicoResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnGradoAcademico.ObtenerPorId(modelo.IdGradoAcademico));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnGradoAcademico.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GradoAcademicoResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(GradoAcademicoResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(GradoAcademicoResponseEliminarDto), 200)]
        public async Task<ActionResult<GradoAcademicoResponseEliminarDto>> Eliminar(int id)
        {
            GradoAcademicoResponseEliminarDto respuesta = new GradoAcademicoResponseEliminarDto();
            var entidad = await Task.FromResult(_lnGradoAcademico.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnGradoAcademico.Eliminar(id));
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
