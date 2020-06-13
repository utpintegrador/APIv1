using System.Threading.Tasks;
using Entidad.Dto.Response.Interaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Interaccion;

namespace App.Controllers.Interaccion
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MatchController : ControllerBase
    {
        private readonly LnMatch _lnMatch = new LnMatch();
        [HttpGet("ObtenerPorIdUsuario/{id}")]
        public async Task<ActionResult<MatchResponseObtenerPorIdUsuarioDto>> ObtenerPorIdUsuario(long id)
        {
            MatchResponseObtenerPorIdUsuarioDto respuesta = new MatchResponseObtenerPorIdUsuarioDto();
            var result = await Task.FromResult(_lnMatch.ObtenerPorIdUsuario(id));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}
