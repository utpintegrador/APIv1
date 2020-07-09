using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;
using Microsoft.AspNetCore.Authorization;
using App.CustomHandler;
using Entidad.Request.Maestro;
using System.Collections.Generic;
using System.Linq;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly LnEstado _lnEstado = new LnEstado();
        private readonly IMapper _mapper;

        public EstadoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseEstadoObtenerDto>> Obtener([FromBody]RequestEstadoObtenerDto filtro)
        {
            ResponseEstadoObtenerDto respuesta = new ResponseEstadoObtenerDto();
            var result = await Task.FromResult(_lnEstado.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [Authorize]
        [HttpGet("{id}", Name = "ObtenerEstadoPorId")]
        [ProducesResponseType(typeof(ResponseEstadoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseEstadoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseEstadoObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseEstadoObtenerPorIdDto respuesta = new ResponseEstadoObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnEstado.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseEstadoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseEstadoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseEstadoRegistrarDto>> Registrar([FromBody] RequestEstadoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseEstadoRegistrarDto respuesta = new ResponseEstadoRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnEstado.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseEstadoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseEstadoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseEstadoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseEstadoModificarDto>> Modificar([FromBody] RequestEstadoModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseEstadoModificarDto respuesta = new ResponseEstadoModificarDto();
            
            var entidad = await Task.FromResult(_lnEstado.ObtenerPorId(modelo.IdEstado));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnEstado.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseEstadoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseEstadoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseEstadoEliminarDto), 200)]
        public async Task<ActionResult<ResponseEstadoEliminarDto>> Eliminar(int id)
        {
            ResponseEstadoEliminarDto respuesta = new ResponseEstadoEliminarDto();
            var entidad = await Task.FromResult(_lnEstado.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnEstado.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo/{idTipoEstado}")]
        [ProducesResponseType(typeof(ResponseEstadoObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseEstadoObtenerComboDto), 400)]
        [ProducesResponseType(typeof(ResponseEstadoObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseEstadoObtenerComboDto>> ObtenerCombo(int idTipoEstado)
        {
            ResponseEstadoObtenerComboDto respuesta = new ResponseEstadoObtenerComboDto();
            if (idTipoEstado == 0)
            {
                respuesta.ListaError = new List<ErrorDto>();
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "IdTipoEstado: parametro es requerido"
                });
                respuesta.ProcesadoOk = 0;
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnEstado.ObtenerCombo(idTipoEstado));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}