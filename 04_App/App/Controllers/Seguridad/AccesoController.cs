using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Seguridad;
using Entidad.Response;
using Entidad.Response.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;

namespace App.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly LnAcceso _lnAcceso = new LnAcceso();
        private readonly IMapper mapper;

        public AccesoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseAccesoObtenerDto>> Obtener(RequestAccesoObtenerDto filtro)
        {
            ResponseAccesoObtenerDto respuesta = new ResponseAccesoObtenerDto();
            var result = await Task.FromResult(_lnAcceso.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerAccesoPorId")]
        [ProducesResponseType(typeof(ResponseAccesoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseAccesoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseAccesoObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseAccesoObtenerPorIdDto respuesta = new ResponseAccesoObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnAcceso.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseAccesoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseAccesoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseAccesoRegistrarDto>> Registrar([FromBody] RequestAccesoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseAccesoRegistrarDto respuesta = new ResponseAccesoRegistrarDto();

            int nuevoId = 0;
            var result = await Task.FromResult(_lnAcceso.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseAccesoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseAccesoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseAccesoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseAccesoModificarDto>> Modificar([FromBody] RequestAccesoModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseAccesoModificarDto respuesta = new ResponseAccesoModificarDto();

            var entidad = await Task.FromResult(_lnAcceso.ObtenerPorId(modelo.IdAcceso));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnAcceso.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseAccesoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseAccesoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseAccesoEliminarDto), 200)]
        public async Task<ActionResult<ResponseAccesoEliminarDto>> Eliminar(int id)
        {
            ResponseAccesoEliminarDto respuesta = new ResponseAccesoEliminarDto();
            var entidad = await Task.FromResult(_lnAcceso.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnAcceso.Eliminar(id));
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