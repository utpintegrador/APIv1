using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Dto.Maestro;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<NegocioUbicacionResponseObtenerPorIdNegocioDto>> ObtenerPorIdNegocio([FromBody] NegocioUbicacionObtenerPorIdNegocioPrmDto filtro)
        {
            NegocioUbicacionResponseObtenerPorIdNegocioDto respuesta = new NegocioUbicacionResponseObtenerPorIdNegocioDto();
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
        [ProducesResponseType(typeof(NegocioUbicacionResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<NegocioUbicacionResponseObtenerPorIdDto>> ObtenerPorId(long id)
        {
            NegocioUbicacionResponseObtenerPorIdDto respuesta = new NegocioUbicacionResponseObtenerPorIdDto();
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
        [ProducesResponseType(typeof(NegocioUbicacionResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<NegocioUbicacionResponseRegistrarDto>> Registrar([FromBody] NegocioUbicacionRegistrarPrmDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            NegocioUbicacionResponseRegistrarDto respuesta = new NegocioUbicacionResponseRegistrarDto();

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
        [ProducesResponseType(typeof(NegocioUbicacionResponseModificarDto), 404)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseModificarDto), 400)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<NegocioUbicacionResponseModificarDto>> Modificar([FromBody] NegocioUbicacionModificarPrmDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            NegocioUbicacionResponseModificarDto respuesta = new NegocioUbicacionResponseModificarDto();

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
        [ProducesResponseType(typeof(NegocioUbicacionResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(NegocioUbicacionResponseEliminarDto), 200)]
        public async Task<ActionResult<NegocioUbicacionResponseEliminarDto>> Eliminar(int id)
        {
            NegocioUbicacionResponseEliminarDto respuesta = new NegocioUbicacionResponseEliminarDto();
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