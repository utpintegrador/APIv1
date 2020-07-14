using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Maestro;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly LnProducto _lnProducto = new LnProducto();
        private readonly IMapper mapper;

        public ProductoController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerPorIdUsuario")]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoObtenerPorIdUsuarioDto>> ObtenerPorIdUsuario([FromBody] RequestProductoObtenerPorIdUsuarioDto filtro)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoObtenerPorIdUsuarioDto respuesta = new ResponseProductoObtenerPorIdUsuarioDto();
            var result = await Task.FromResult(_lnProducto.ObtenerPorIdUsuario(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpPost("ObtenerPorIdNegocio")]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoObtenerPorIdNegocioDto>> ObtenerPorIdNegocio([FromBody] RequestProductoObtenerPorIdNegocioDto filtro)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoObtenerPorIdNegocioDto respuesta = new ResponseProductoObtenerPorIdNegocioDto();
            var result = await Task.FromResult(_lnProducto.ObtenerPorIdNegocio(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerProductoPorId")]
        [ProducesResponseType(typeof(ResponseProductoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseProductoObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponseProductoObtenerPorIdDto respuesta = new ResponseProductoObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnProducto.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseProductoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoRegistrarDto>> Registrar([FromBody] RequestProductoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoRegistrarDto respuesta = new ResponseProductoRegistrarDto();

            long nuevoId = 0;
            var result = await Task.FromResult(_lnProducto.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(ResponseProductoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoModificarDto>> Modificar([FromBody] RequestProductoModificarDto modelo)
        {

            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoModificarDto respuesta = new ResponseProductoModificarDto();

            var entidad = await Task.FromResult(_lnProducto.ObtenerPorId(modelo.IdProducto));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnProducto.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseProductoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoEliminarDto), 200)]
        public async Task<ActionResult<ResponseProductoEliminarDto>> Eliminar(long id)
        {
            ResponseProductoEliminarDto respuesta = new ResponseProductoEliminarDto();
            var entidad = await Task.FromResult(_lnProducto.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnProducto.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerPorIdConAtributos/{id}")]
        [ProducesResponseType(typeof(ResponseProductoObtenerPorIdConAtributosDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoObtenerPorIdConAtributosDto), 200)]
        public async Task<ActionResult<ResponseProductoObtenerPorIdConAtributosDto>> ObtenerPorIdConAtributos(long id)
        {
            ResponseProductoObtenerPorIdConAtributosDto respuesta = new ResponseProductoObtenerPorIdConAtributosDto();
            var entidad = await Task.FromResult(_lnProducto.ObtenerPorIdConAtributos(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpPost("EliminarMasivo")]
        [ProducesResponseType(typeof(ResponseProductoEliminarMasivoDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoEliminarMasivoDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoEliminarMasivoDto>> EliminarMasivo([FromBody] RequestProductoEliminarMasivoDto prm)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoEliminarMasivoDto respuesta = new ResponseProductoEliminarMasivoDto();

            var result = await Task.FromResult(_lnProducto.EliminarMasivo(prm));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;

            return Ok(respuesta);

        }

    }
}