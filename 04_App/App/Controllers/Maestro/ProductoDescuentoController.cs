using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Maestro;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoDescuentoController : ControllerBase
    {
        private readonly LnProductoDescuento _lnProductoDescuento = new LnProductoDescuento();
        private readonly IMapper mapper;

        public ProductoDescuentoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerPorIdProducto")]
        [ProducesResponseType(typeof(ResponseProductoDescuentoObtenerPorIdProductoDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoObtenerPorIdProductoDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoDescuentoObtenerPorIdProductoDto>> ObtenerPorIdNegocio([FromBody] RequestProductoDescuentoObtenerPorIdProductoDto filtro)
        {
            ResponseProductoDescuentoObtenerPorIdProductoDto respuesta = new ResponseProductoDescuentoObtenerPorIdProductoDto();
            var result = await Task.FromResult(_lnProductoDescuento.ObtenerPorIdProducto(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }
    }
}