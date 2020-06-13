using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Global;
using Entidad.Dto.Maestro;
using Entidad.Dto.Response.Maestro;
using Entidad.Entidad.Maestro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEstadoController : ControllerBase
    {
        private readonly LnTipoEstado _lnTipoEstado = new LnTipoEstado();
        private readonly IMapper mapper;

        public TipoEstadoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TipoEstadoResponseObtenerDto>> Obtener()
        {
            TipoEstadoResponseObtenerDto respuesta = new TipoEstadoResponseObtenerDto();
            var result = await Task.FromResult(_lnTipoEstado.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerTipoEstadoPorId")]
        [ProducesResponseType(typeof(TipoEstadoResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(TipoEstadoResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<TipoEstadoResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            TipoEstadoResponseObtenerPorIdDto respuesta = new TipoEstadoResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(TipoEstadoResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(TipoEstadoResponseRegistrarDto), 200)]
        public async Task<ActionResult<TipoEstadoResponseRegistrarDto>> Registrar([FromBody] TipoEstadoRegistrarDto modelo)
        {
            TipoEstadoResponseRegistrarDto respuesta = new TipoEstadoResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnTipoEstado.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(TipoEstadoResponseModificarDto), 404)]
        [ProducesResponseType(typeof(TipoEstadoResponseModificarDto), 400)]
        [ProducesResponseType(typeof(TipoEstadoResponseModificarDto), 200)]
        public async Task<ActionResult<TipoEstadoResponseModificarDto>> Modificar([FromBody] TipoEstado modelo)
        {
            TipoEstadoResponseModificarDto respuesta = new TipoEstadoResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(modelo.IdTipoEstado));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnTipoEstado.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TipoEstadoResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(TipoEstadoResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(TipoEstadoResponseEliminarDto), 200)]
        public async Task<ActionResult<TipoEstadoResponseEliminarDto>> Eliminar(int id)
        {
            TipoEstadoResponseEliminarDto respuesta = new TipoEstadoResponseEliminarDto();
            var entidad = await Task.FromResult(_lnTipoEstado.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnTipoEstado.Eliminar(id));
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