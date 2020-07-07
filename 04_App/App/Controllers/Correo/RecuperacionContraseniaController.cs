using System.Threading.Tasks;
using App.CustomHandler;
using Entidad.Dto.Correo;
using Entidad.Request.Correo;
using Entidad.Response;
using Entidad.Response.Correo;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Servicio;

namespace App.Controllers.Correo
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperacionContraseniaController : ControllerBase
    {
        private readonly LnRecuperacionContrasenia _lnRecuperacionContrasenia = new LnRecuperacionContrasenia();

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseRecuperacionContraseniaRegistrarDto>> Registrar([FromBody] RequestRecuperacionContraseniaRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseRecuperacionContraseniaRegistrarDto respuesta = new ResponseRecuperacionContraseniaRegistrarDto();
            
            var result = await Task.FromResult(_lnRecuperacionContrasenia.Registrar(modelo.CorreoElectronico));
            if (result == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            if (result.NumeroRespuesta == 1)
            {
                respuesta.ProcesadoOk = 1;
            }
            respuesta.Cuerpo = result;

            return Ok(respuesta);

        }

        [HttpGet("ObtenerUsuarioPorCodigo/{codigo}")] 
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaObtenerPorCodigoDto), 404)]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaObtenerPorCodigoDto), 200)]
        public async Task<ActionResult<RecuperacionContraseniaObtenerPorCodigoDto>> ObtenerUsuarioPorCodigo(string codigo)
        {
            ResponseRecuperacionContraseniaObtenerPorCodigoDto respuesta = new ResponseRecuperacionContraseniaObtenerPorCodigoDto();
            var listado = await Task.FromResult(_lnRecuperacionContrasenia.ObtenerUsuarioPorCodigo(codigo));
            if (listado == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = listado;
            return Ok(respuesta);
        }


        [HttpPut("ModificarContraseniaMedianteCodigo")]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaModificarContraseniaDto), 404)]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaModificarContraseniaDto), 400)]
        [ProducesResponseType(typeof(ResponseRecuperacionContraseniaModificarContraseniaDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseRecuperacionContraseniaModificarContraseniaDto>> ModificarContraseniaMedianteCodigo([FromBody] RequestRecuperacionContraseniaModificarContraseniaDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseRecuperacionContraseniaModificarContraseniaDto respuesta = new ResponseRecuperacionContraseniaModificarContraseniaDto();
            
            var result = await Task.FromResult(_lnRecuperacionContrasenia.ModificarContraseniaMedianteCodigo(modelo));
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
