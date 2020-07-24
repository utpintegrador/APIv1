using System;
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

        [HttpGet("{id}", Name = "ObtenerProductoDescuentoPorId")]
        [ProducesResponseType(typeof(ResponseProductoDescuentoObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseProductoDescuentoObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponseProductoDescuentoObtenerPorIdDto respuesta = new ResponseProductoDescuentoObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnProductoDescuento.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(ResponseProductoDescuentoRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoDescuentoRegistrarDto>> Registrar([FromBody] RequestProductoDescuentoRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoDescuentoRegistrarDto respuesta = new ResponseProductoDescuentoRegistrarDto();

            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;
            bool valFechaInicio = CustomValidation.ValidacionFecha.ValidarFechaNula(modelo.FechaInicio, ref fechaInicio);
            bool valFechaFin = CustomValidation.ValidacionFecha.ValidarFechaNula(modelo.FechaFin, ref fechaFin);
            if(!valFechaInicio || !valFechaFin)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El formato de fecha debe ser [yyyy/MM/dd] o [yyyy/MM/dd HH:mm:ss]" });
                return BadRequest(respuesta);
            }

            modelo.FechaInicioDate = fechaInicio;
            modelo.FechaFinDate = fechaFin;

            long nuevoId = 0;
            var result = await Task.FromResult(_lnProductoDescuento.Registrar(modelo, ref nuevoId));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;

            return Ok(respuesta);

        }

        [HttpPut()]
        [ProducesResponseType(typeof(ResponseProductoDescuentoModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoDescuentoModificarDto>> Modificar([FromBody] RequestProductoDescuentoModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseProductoDescuentoModificarDto respuesta = new ResponseProductoDescuentoModificarDto();
            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;
            bool valFechaInicio = CustomValidation.ValidacionFecha.ValidarFechaNula(modelo.FechaInicio, ref fechaInicio);
            bool valFechaFin = CustomValidation.ValidacionFecha.ValidarFechaNula(modelo.FechaFin, ref fechaFin);
            if (!valFechaInicio || !valFechaFin)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El formato de fecha debe ser [dd/MM/yyyy] o [dd/MM/yyyy HH:mm:ss]" });
                return BadRequest(respuesta);
            }

            modelo.FechaInicioDate = fechaInicio;
            modelo.FechaFinDate = fechaFin;

            var entidad = await Task.FromResult(_lnProductoDescuento.ObtenerPorId(modelo.IdProductoDescuento));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnProductoDescuento.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseProductoDescuentoEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoDescuentoEliminarDto), 200)]
        public async Task<ActionResult<ResponseProductoDescuentoEliminarDto>> Eliminar(long id)
        {
            ResponseProductoDescuentoEliminarDto respuesta = new ResponseProductoDescuentoEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnProductoDescuento.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnProductoDescuento.Eliminar(id));
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