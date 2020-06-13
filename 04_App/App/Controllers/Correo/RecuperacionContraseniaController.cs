using System.Threading.Tasks;
using Entidad.Dto.Correo;
using Entidad.Dto.Global;
using Entidad.Dto.Response.Correo;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Servicio;

namespace App.Controllers.Correo
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RecuperacionContraseniaController : ControllerBase
    {
        private readonly LnRecuperacionContrasenia _lnRecuperacionContrasenia = new LnRecuperacionContrasenia();

        [HttpPost]
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseRegistrarDto), 200)]
        public async Task<ActionResult<RecuperacionContraseniaResponseRegistrarDto>> Registrar([FromBody] RecuperacionContraseniaRegistrarDto modelo)
        {
            RecuperacionContraseniaResponseRegistrarDto respuesta = new RecuperacionContraseniaResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseObtenerPorCodigoDto), 404)]
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseObtenerPorCodigoDto), 200)]
        public async Task<ActionResult<RecuperacionContraseniaObtenerPorCodigoDto>> ObtenerUsuarioPorCodigo(string codigo)
        {
            RecuperacionContraseniaResponseObtenerPorCodigoDto respuesta = new RecuperacionContraseniaResponseObtenerPorCodigoDto();
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
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseModificarContraseniaDto), 404)]
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseModificarContraseniaDto), 400)]
        [ProducesResponseType(typeof(RecuperacionContraseniaResponseModificarContraseniaDto), 200)]
        public async Task<ActionResult<RecuperacionContraseniaResponseModificarContraseniaDto>> ModificarContraseniaMedianteCodigo([FromBody] RecuperacionContraseniaModificarContraseniaDto modelo)
        {
            RecuperacionContraseniaResponseModificarContraseniaDto respuesta = new RecuperacionContraseniaResponseModificarContraseniaDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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
