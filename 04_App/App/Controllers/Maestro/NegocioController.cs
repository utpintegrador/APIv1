using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<NegocioResponseObtenerDto>> Obtener([FromBody] NegocioObtenerFiltroDto filtro)
        {
            NegocioResponseObtenerDto respuesta = new NegocioResponseObtenerDto();
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
        [ProducesResponseType(typeof(NegocioResponseObtenerCercanosDto), 200)]
        public async Task<ActionResult<NegocioResponseObtenerCercanosDto>> ObtenerCercanos([FromBody] NegocioObtenerCercanosFiltroDto filtro)
        {
            NegocioResponseObtenerCercanosDto respuesta = new NegocioResponseObtenerCercanosDto();
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
                    respuesta.CantidadTotalRegistros = result.First().TotalItems;
                }

                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerNegocioPorId")]
        [ProducesResponseType(typeof(NegocioResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(NegocioResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<NegocioResponseObtenerPorIdDto>> ObtenerPorId(long id)
        {
            NegocioResponseObtenerPorIdDto respuesta = new NegocioResponseObtenerPorIdDto();
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
        [ProducesResponseType(typeof(NegocioResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(NegocioResponseRegistrarDto), 200)]
        public async Task<ActionResult<NegocioResponseRegistrarDto>> Registrar([FromBody] NegocioRegistrarDto modelo)
        {
            NegocioResponseRegistrarDto respuesta = new NegocioResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
        [ProducesResponseType(typeof(NegocioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(NegocioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(NegocioResponseModificarDto), 200)]
        public async Task<ActionResult<NegocioResponseModificarDto>> Modificar([FromBody] NegocioModificarDto modelo)
        {
            NegocioResponseModificarDto respuesta = new NegocioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
        [ProducesResponseType(typeof(NegocioResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(NegocioResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(NegocioResponseEliminarDto), 200)]
        public async Task<ActionResult<NegocioResponseEliminarDto>> Eliminar(int id)
        {
            NegocioResponseEliminarDto respuesta = new NegocioResponseEliminarDto();
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
    }
}
