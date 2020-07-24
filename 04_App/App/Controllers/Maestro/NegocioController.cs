using System.Collections.Generic;
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
    public class NegocioController : ControllerBase
    {
        private readonly LnNegocio _lnNegocio = new LnNegocio();
        private readonly IMapper mapper;

        public NegocioController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseNegocioObtenerDto>> Obtener([FromBody] RequestNegocioObtenerDto filtro)
        {
            ResponseNegocioObtenerDto respuesta = new ResponseNegocioObtenerDto();
            var result = await Task.FromResult(_lnNegocio.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        /// <summary>
        /// El parametro IdUsuario es obligatorio, pues se listaran los cercanos a ese usuario
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpPost("ObtenerCercanos")]
        [ProducesResponseType(typeof(ResponseNegocioObtenerCercanosDto), 200)]
        [ProducesResponseType(typeof(ResponseNegocioObtenerCercanosDto), 400)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseNegocioObtenerCercanosDto>> ObtenerCercanos([FromBody] RequestNegocioObtenerCercanosDto filtro)
        {
            if (!ModelState.IsValid) return BadRequest();

            ResponseNegocioObtenerCercanosDto respuesta = new ResponseNegocioObtenerCercanosDto();
            if (filtro.IdUsuario == 0)
            {
                respuesta.ListaError = new List<ErrorDto>();
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "El parametro IdUsuario es requerido"
                });
                respuesta.CantidadTotalRegistros = 0;
                respuesta.ProcesadoOk = 0;
                respuesta.Cuerpo = null;
            }
            else
            {
                var result = await Task.FromResult(_lnNegocio.ObtenerCercanos(filtro));
                respuesta.ProcesadoOk = 1;
                respuesta.Cuerpo = result;

                if (result.Any())
                {
                    respuesta.CantidadTotalRegistros = result.Count();
                }

                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerNegocioPorId")]
        [ProducesResponseType(typeof(ResponseNegocioObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseNegocioObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponseNegocioObtenerPorIdDto respuesta = new ResponseNegocioObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnNegocio.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseNegocioRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseNegocioRegistrarDto>> Registrar([FromBody] RequestNegocioRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseNegocioRegistrarDto respuesta = new ResponseNegocioRegistrarDto();

            long nuevoId = 0;
            var result = await Task.FromResult(_lnNegocio.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseNegocioModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseNegocioModificarDto>> Modificar([FromBody] RequestNegocioModificarDto modelo)
        {
            
            if (!ModelState.IsValid) return BadRequest();
            ResponseNegocioModificarDto respuesta = new ResponseNegocioModificarDto();

            var entidad = await Task.FromResult(_lnNegocio.ObtenerPorId(modelo.IdNegocio));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnNegocio.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseNegocioEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseNegocioEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioEliminarDto), 200)]
        public async Task<ActionResult<ResponseNegocioEliminarDto>> Eliminar(long id)
        {
            ResponseNegocioEliminarDto respuesta = new ResponseNegocioEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnNegocio.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnNegocio.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerCombo/{idUsuario}")]
        [ProducesResponseType(typeof(ResponseNegocioObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseNegocioObtenerComboDto), 400)]
        [ProducesResponseType(typeof(ResponseNegocioObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseNegocioObtenerComboDto>> ObtenerCombo(long idUsuario)
        {
            ResponseNegocioObtenerComboDto respuesta = new ResponseNegocioObtenerComboDto();
            if (idUsuario == 0)
            {
                respuesta.ListaError = new List<ErrorDto>();
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "IdUsuario: parametro es requerido"
                });
                respuesta.ProcesadoOk = 0;
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnNegocio.ObtenerCombo(idUsuario, 0));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}
