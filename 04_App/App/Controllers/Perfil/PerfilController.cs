using System.Threading.Tasks;
using Entidad.Dto.Global;
using Entidad.Dto.Perfil;
using Entidad.Dto.Response.Perfil;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Perfil;

namespace App.Controllers.Perfil
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PerfilController : ControllerBase
    {
        private readonly LnPerfil _lnPerfil = new LnPerfil();

        [HttpGet("{id}", Name = "ObtenerPerfilPorId")]
        [ProducesResponseType(typeof(PerfilResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerPorIdDto>> ObtenerPorId(long id)
        {
            PerfilResponseObtenerPorIdDto respuesta = new PerfilResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(PerfilResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseRegistrarDto), 200)]
        public async Task<ActionResult<PerfilResponseRegistrarDto>> Registrar([FromBody] PerfilRegistrarDto modelo)
        {
            PerfilResponseRegistrarDto respuesta = new PerfilResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnPerfil.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(PerfilResponseModificarDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseModificarDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseModificarDto), 200)]
        public async Task<ActionResult<PerfilResponseModificarDto>> Modificar([FromBody] PerfilModificarDto modelo)
        {
            PerfilResponseModificarDto respuesta = new PerfilResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPerfil.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        [HttpGet("ObtenerPorIdUsuario/{idUsuario}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerPorIdUsuarioDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerPorIdUsuarioDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerPorIdUsuarioDto>> ObtenerPorIdUsuario(long idUsuario)
        {
            PerfilResponseObtenerPorIdUsuarioDto respuesta = new PerfilResponseObtenerPorIdUsuarioDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorIdUsuario(idUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }


        [HttpGet("ObtenerDatoAcademicoPorId/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerDatoAcademicoPorIdDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerDatoAcademicoPorIdDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerDatoAcademicoPorIdDto>> ObtenerDatoAcademicoPorId(long id)
        {
            PerfilResponseObtenerDatoAcademicoPorIdDto respuesta = new PerfilResponseObtenerDatoAcademicoPorIdDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerDatoAcademicoPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }


        [HttpGet("ObtenerDatoLaboralPorId/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerDatoLaboralPorIdDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerDatoLaboralPorIdDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerDatoLaboralPorIdDto>> ObtenerDatoLaboralPorId(long id)
        {
            PerfilResponseObtenerDatoLaboralPorIdDto respuesta = new PerfilResponseObtenerDatoLaboralPorIdDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerDatoLaboralPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }


        [HttpGet("ObtenerInformacionPorId/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerInformacionPorIdDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerInformacionPorIdDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerInformacionPorIdDto>> ObtenerInformacionPorId(long id)
        {
            PerfilResponseObtenerInformacionPorIdDto respuesta = new PerfilResponseObtenerInformacionPorIdDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerInformacionPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }


        [HttpGet("ObtenerInteresesPorId/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerInteresesPorIdDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerInteresesPorIdDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerInteresesPorIdDto>> ObtenerInteresesPorId(long id)
        {
            PerfilResponseObtenerInteresesPorIdDto respuesta = new PerfilResponseObtenerInteresesPorIdDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerInteresesPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }


        //[HttpGet("ObtenerUrlImagenPorId/{id}")]
        //[ProducesResponseType(typeof(PerfilResponseObtenerUrlImagenPorIdDto), 404)]
        //[ProducesResponseType(typeof(PerfilResponseObtenerUrlImagenPorIdDto), 200)]
        //public async Task<ActionResult<PerfilResponseObtenerUrlImagenPorIdDto>> ObtenerUrlImagenPorId(long id)
        //{
        //    PerfilResponseObtenerUrlImagenPorIdDto respuesta = new PerfilResponseObtenerUrlImagenPorIdDto();
        //    var entidad = await Task.FromResult(_lnPerfil.ObtenerUrlImagenPorId(id));
        //    if (entidad == null)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
        //        return NotFound(respuesta);
        //    }

        //    respuesta.ProcesadoOk = 1;
        //    respuesta.Cuerpo = entidad;
        //    return Ok(respuesta);
        //}


        [HttpPut("ModificarDatoAcademico")]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoAcademicoDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoAcademicoDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoAcademicoDto), 200)]
        public async Task<ActionResult<PerfilResponseModificarDatoAcademicoDto>> ModificarDatoAcademico([FromBody] PerfilModificarDatoAcademicoDto modelo)
        {
            PerfilResponseModificarDatoAcademicoDto respuesta = new PerfilResponseModificarDatoAcademicoDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPerfil.ModificarDatoAcademico(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        [HttpPut("ModificarDatoLaboral")]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoLaboralDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoLaboralDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseModificarDatoLaboralDto), 200)]
        public async Task<ActionResult<PerfilResponseModificarDatoLaboralDto>> ModificarDatoLaboral([FromBody] PerfilModificarDatoLaboralDto modelo)
        {
            PerfilResponseModificarDatoLaboralDto respuesta = new PerfilResponseModificarDatoLaboralDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPerfil.ModificarDatoLaboral(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        [HttpPut("ModificarInformacion")]
        [ProducesResponseType(typeof(PerfilResponseModificarInformacionDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseModificarInformacionDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseModificarInformacionDto), 200)]
        public async Task<ActionResult<PerfilResponseModificarInformacionDto>> ModificarInformacion([FromBody] PerfilModificarInformacionDto modelo)
        {
            PerfilResponseModificarInformacionDto respuesta = new PerfilResponseModificarInformacionDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPerfil.ModificarInformacion(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        [HttpPut("ModificarIntereses")]
        [ProducesResponseType(typeof(PerfilResponseModificarInteresesDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseModificarInteresesDto), 400)]
        [ProducesResponseType(typeof(PerfilResponseModificarInteresesDto), 200)]
        public async Task<ActionResult<PerfilResponseModificarInteresesDto>> ModificarIntereses([FromBody] PerfilModificarInteresesDto modelo)
        {
            PerfilResponseModificarInteresesDto respuesta = new PerfilResponseModificarInteresesDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnPerfil.ModificarIntereses(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }


        //[HttpPut("ModificarUrlImagen")]
        //[ProducesResponseType(typeof(PerfilResponseModificarUrlImagenDto), 404)]
        //[ProducesResponseType(typeof(PerfilResponseModificarUrlImagenDto), 400)]
        //[ProducesResponseType(typeof(PerfilResponseModificarUrlImagenDto), 200)]
        //public async Task<ActionResult<PerfilResponseModificarUrlImagenDto>> ModificarUrlImagen([FromBody] PerfilModificarUrlImagenDto modelo)
        //{
        //    PerfilResponseModificarUrlImagenDto respuesta = new PerfilResponseModificarUrlImagenDto();
        //    if (!ModelState.IsValid)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
        //        return BadRequest(respuesta);
        //    }

        //    var entidad = await Task.FromResult(_lnPerfil.ObtenerPorId(modelo.IdPerfil));
        //    if (entidad == null)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
        //        return NotFound(respuesta);
        //    }

        //    var result = await Task.FromResult(_lnPerfil.ModificarUrlImagen(modelo));
        //    if (result == 0)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
        //        return BadRequest(respuesta);
        //    }

        //    respuesta.ProcesadoOk = 1;
        //    return Ok(respuesta);
        //}


        [HttpGet("ObtenerPasarelaPorIdUsuario/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerPasarelaDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerPasarelaDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerPasarelaDto>> ObtenerPasarelaPorIdUsuario(long id)
        {
            PerfilResponseObtenerPasarelaDto respuesta = new PerfilResponseObtenerPasarelaDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerPasarela(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerInformacionCompletaPorIdUsuario/{id}")]
        [ProducesResponseType(typeof(PerfilResponseObtenerInformacionCompletaDto), 404)]
        [ProducesResponseType(typeof(PerfilResponseObtenerInformacionCompletaDto), 200)]
        public async Task<ActionResult<PerfilResponseObtenerInformacionCompletaDto>> ObtenerInformacionCompletaPorIdUsuario(long id)
        {
            PerfilResponseObtenerInformacionCompletaDto respuesta = new PerfilResponseObtenerInformacionCompletaDto();
            var entidad = await Task.FromResult(_lnPerfil.ObtenerInformacionCompleta(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

    }
}
