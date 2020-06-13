using System.Threading.Tasks;
using AutoMapper;
using Entidad.Dto.Global;
using Entidad.Dto.Response.Interaccion;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Interaccion;

namespace App.Controllers.Interaccion
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ConversacionController : ControllerBase
    {
        private readonly LnConversacion _lnConversacion = new LnConversacion();
        private readonly IMapper _mapper;

        public ConversacionController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet("ObtenerPorIdUsuario/{id}")]
        [ProducesResponseType(typeof(ConversacionResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ConversacionResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<ConversacionResponseObtenerPorIdDto>> ObtenerPorIdUsuario(long id)
        {
            ConversacionResponseObtenerPorIdDto respuesta = new ConversacionResponseObtenerPorIdDto();
            var listado = await Task.FromResult(_lnConversacion.ObtenerPorIdUsuario(id));
            if (listado == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = listado;
            return Ok(respuesta);
        }
    }
}
