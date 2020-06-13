using System.Threading.Tasks;
using Entidad.Dto.Global;
using Entidad.Dto.Interaccion;
using Entidad.Dto.Response.Interaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Interaccion;

namespace App.Controllers.Interaccion
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PublicacionMeGustaController : ControllerBase
    {
        private readonly LnPublicacionMeGusta _lnPublicacionMeGusta = new LnPublicacionMeGusta();
        [HttpPost]
        [ProducesResponseType(typeof(PublicacionMeGustaResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(PublicacionMeGustaResponseRegistrarDto), 200)]
        public async Task<ActionResult<PublicacionMeGustaResponseRegistrarDto>> Registrar([FromBody] PublicacionMeGustaRegistrarDto modelo)
        {
            PublicacionMeGustaResponseRegistrarDto respuesta = new PublicacionMeGustaResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnPublicacionMeGusta.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }
    }
}
