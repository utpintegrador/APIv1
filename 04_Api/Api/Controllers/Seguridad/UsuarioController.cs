using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Entidad.Dto.Global;
using Entidad.Dto.Response.Seguridad;
using Entidad.Dto.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;

namespace Api.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = 1)]
    public class UsuarioController : ControllerBase
    {
        private readonly LnUsuario _lnUsuario = new LnUsuario();
        private readonly IMapper _mapper;

        public UsuarioController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioResponseObtenerDto>> Obtener()
        {
            UsuarioResponseObtenerDto respuesta = new UsuarioResponseObtenerDto();
            var result = await Task.FromResult(_lnUsuario.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerUsuarioPorId")]
        [ProducesResponseType(typeof(UsuarioResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<UsuarioResponseObtenerPorIdDto>> ObtenerPorId(long id)
        {
            UsuarioResponseObtenerPorIdDto respuesta = new UsuarioResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 200)]
        public async Task<ActionResult<UsuarioResponseEliminarDto>> Eliminar(long id)
        {
            UsuarioResponseEliminarDto respuesta = new UsuarioResponseEliminarDto();
            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnUsuario.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseRegistrarDto), 200)]
        public async Task<ActionResult<UsuarioResponseRegistrarDto>> Registrar([FromBody] UsuarioRegistrarDto modelo)
        {
            UsuarioResponseRegistrarDto respuesta = new UsuarioResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnUsuario.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 200)]
        public async Task<ActionResult<UsuarioResponseModificarDto>> Modificar([FromBody] UsuarioModificarDto modelo)
        {
            UsuarioResponseModificarDto respuesta = new UsuarioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(modelo.IdUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnUsuario.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }



        /****************************************************************************/
        [HttpPut("ModificarContrasenia")]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 200)]
        public async Task<ActionResult<UsuarioResponseModificarDto>> ModificarContrasenia([FromBody] UsuarioCambioContraseniaDto modelo)
        {
            UsuarioResponseModificarDto respuesta = new UsuarioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(modelo.IdUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnUsuario.ModificarContrasenia(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

    }
}
