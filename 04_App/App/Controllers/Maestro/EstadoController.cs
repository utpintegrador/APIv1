using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Dto.Maestro;
using Entidad.Response.Maestro;
using Entidad.Entidad.Maestro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

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

        [HttpGet]
        public async Task<ActionResult<EstadoResponseObtenerDto>> Obtener()
        {
            EstadoResponseObtenerDto respuesta = new EstadoResponseObtenerDto();
            var result = await Task.FromResult(_lnEstado.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerEstadoPorId")]
        [ProducesResponseType(typeof(EstadoResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(EstadoResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<EstadoResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            EstadoResponseObtenerPorIdDto respuesta = new EstadoResponseObtenerPorIdDto();
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


        [HttpGet("ObtenerPorIdTipoEstado/{id}")]
        [ProducesResponseType(typeof(EstadoResponseObtenerPorIdTipoEstadoDto), 404)]
        [ProducesResponseType(typeof(EstadoResponseObtenerPorIdTipoEstadoDto), 200)]
        public async Task<ActionResult<EstadoResponseObtenerPorIdTipoEstadoDto>> ObtenerPorIdTipoEstado(int id)
        {
            EstadoResponseObtenerPorIdTipoEstadoDto respuesta = new EstadoResponseObtenerPorIdTipoEstadoDto();
            var result = await Task.FromResult(_lnEstado.ObtenerPorIdTipoEstado(id));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }


        [HttpPost]
        [ProducesResponseType(typeof(EstadoResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(EstadoResponseRegistrarDto), 200)]
        public async Task<ActionResult<EstadoResponseRegistrarDto>> Registrar([FromBody] EstadoRegistrarDto modelo)
        {
            EstadoResponseRegistrarDto respuesta = new EstadoResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
        [ProducesResponseType(typeof(EstadoResponseModificarDto), 404)]
        [ProducesResponseType(typeof(EstadoResponseModificarDto), 400)]
        [ProducesResponseType(typeof(EstadoResponseModificarDto), 200)]
        public async Task<ActionResult<EstadoResponseModificarDto>> Modificar([FromBody] Estado modelo)
        {
            EstadoResponseModificarDto respuesta = new EstadoResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
        [ProducesResponseType(typeof(EstadoResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(EstadoResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(EstadoResponseEliminarDto), 200)]
        public async Task<ActionResult<EstadoResponseEliminarDto>> Eliminar(int id)
        {
            EstadoResponseEliminarDto respuesta = new EstadoResponseEliminarDto();
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
    }
}