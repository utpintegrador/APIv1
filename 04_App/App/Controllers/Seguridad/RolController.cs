using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Seguridad;
using Entidad.Response;
using Entidad.Response.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;

namespace App.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly LnRol _lnRol = new LnRol();
        private readonly IMapper mapper;

        public RolController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseRolObtenerDto>> Obtener(RequestRolObtenerDto filtro)
        {
            ResponseRolObtenerDto respuesta = new ResponseRolObtenerDto();
            var result = await Task.FromResult(_lnRol.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}", Name = "ObtenerRolPorId")]
        [ProducesResponseType(typeof(ResponseRolObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseRolObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseRolObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseRolObtenerPorIdDto respuesta = new ResponseRolObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnRol.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRolRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseRolRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseRolRegistrarDto>> Registrar([FromBody] RequestRolRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseRolRegistrarDto respuesta = new ResponseRolRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnRol.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [Authorize(Roles = "Administrador")]
        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(ResponseRolModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseRolModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseRolModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseRolModificarDto>> Modificar([FromBody] RequestRolModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseRolModificarDto respuesta = new ResponseRolModificarDto();

            var entidad = await Task.FromResult(_lnRol.ObtenerPorId(modelo.IdRol));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnRol.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseRolEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseRolEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseRolEliminarDto), 200)]
        public async Task<ActionResult<ResponseRolEliminarDto>> Eliminar(int id)
        {
            ResponseRolEliminarDto respuesta = new ResponseRolEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }
            
            var entidad = await Task.FromResult(_lnRol.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnRol.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseRolObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseRolObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseRolObtenerComboDto>> ObtenerCombo()
        {
            ResponseRolObtenerComboDto respuesta = new ResponseRolObtenerComboDto();

            var result = await Task.FromResult(_lnRol.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}