using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;

namespace App.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly LnRol _lnRol = new LnRol();
        private readonly IMapper mapper;

        public RolController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet("ObtenerCombo")]
        [ProducesResponseType(typeof(ResponseRolObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseRolObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseRolObtenerComboDto>> ObtenerCombo()
        {
            ResponseRolObtenerComboDto respuesta = new ResponseRolObtenerComboDto();

            var result = await Task.FromResult(_lnRol.ObtenerCombo());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }
    }
}