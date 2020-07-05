using System;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Seguridad;
using Entidad.Dto.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace App.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = 1)]
    public class UsuarioController : ControllerBase
    {
        private readonly LnUsuario _lnUsuario = new LnUsuario();
        private readonly IMapper _mapper;

        public UsuarioController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<UsuarioResponseObtenerDto>> Obtener([FromBody] UsuarioObtenerFiltroDto filtro)
        {
            UsuarioResponseObtenerDto respuesta = new UsuarioResponseObtenerDto();
            var result = await Task.FromResult(_lnUsuario.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerUsuarioPorId")]
        [ProducesResponseType(typeof(UsuarioResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<UsuarioResponseObtenerPorIdDto>> ObtenerPorId(long id)
        {
            UsuarioResponseObtenerPorIdDto respuesta = new UsuarioResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = entidad;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarDto), 200)]
        public async Task<ActionResult<UsuarioResponseEliminarDto>> Eliminar(long id)
        {
            UsuarioResponseEliminarDto respuesta = new UsuarioResponseEliminarDto();
            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnUsuario.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseRegistrarDto), 200)]
        public async Task<ActionResult<UsuarioResponseRegistrarDto>> Registrar([FromBody] UsuarioRegistrarDto modelo)
        {
            UsuarioResponseRegistrarDto respuesta = new UsuarioResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            long nuevoId = 0;
            var result = await Task.FromResult(_lnUsuario.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 200)]
        public async Task<ActionResult<UsuarioResponseModificarDto>> Modificar([FromBody] UsuarioModificarDto modelo)
        {
            UsuarioResponseModificarDto respuesta = new UsuarioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(modelo.IdUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnUsuario.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        

        /****************************************************************************/
        [HttpPut("ModificarContrasenia")]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseModificarDto), 200)]
        public async Task<ActionResult<UsuarioResponseModificarDto>> ModificarContrasenia([FromBody] UsuarioCambioContraseniaDto modelo)
        {
            UsuarioResponseModificarDto respuesta = new UsuarioResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnUsuario.ObtenerPorId(modelo.IdUsuario));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnUsuario.ModificarContrasenia(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        /// <summary>
        /// modelo.ArchivoBytes = byte[]
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("ImagenMetodo1")]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 200)]
        public async Task<ActionResult<UsuarioResponseSubirImagenDto>> ImagenMetodo1([FromBody] UsuarioModificarImagenMetodo1FiltroDto modelo)
        {
            string urlImagenNueva = string.Empty;

            UsuarioResponseSubirImagenDto respuesta = new UsuarioResponseSubirImagenDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            if(modelo == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            if (modelo.ArchivoBytes == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El archivo de bytes es requerido" });
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo, ref urlImagenNueva));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result == -1)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdUsuario proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpPost("ImagenMetodo2")]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 200)]
        public async Task<ActionResult<UsuarioResponseSubirImagenDto>> ImagenMetodo2(IFormFile archivo, long idUsuario)
        {
            UsuarioResponseSubirImagenDto respuesta = new UsuarioResponseSubirImagenDto();
            try
            {
                if (archivo == null || idUsuario == 0)
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                    return BadRequest(respuesta);
                }

                //transformar IFormFile hacia bytes
                var file = archivo;
                if (file.Length == 0)
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                    return BadRequest(respuesta);
                }

                string urlImagenNueva = string.Empty;
                var nombreArchivo = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string extension = Path.GetExtension(nombreArchivo).Trim().Replace(".", string.Empty).ToLower();
                byte[] archivoBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    archivoBytes = memoryStream.ToArray();
                }
                UsuarioModificarImagenMetodo1FiltroDto modelo = new UsuarioModificarImagenMetodo1FiltroDto
                {
                    ArchivoBytes = archivoBytes,
                    ExtensionSinPunto = extension,
                    IdUsuario = idUsuario
                };
                var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo, ref urlImagenNueva));
                if (result == 0)
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                    return BadRequest(respuesta);
                }

                respuesta.ProcesadoOk = 1;
                respuesta.UrlImagen = urlImagenNueva;

                return Ok(respuesta);
            }
            catch (InvalidOperationException invEx)
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdUsuario' deben ser enviados mediante 'multipart/form-data'"
                });
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = (string.IsNullOrEmpty(invEx.StackTrace) ? invEx.Message : invEx.StackTrace).Replace(Environment.NewLine, " ")
                });
                return BadRequest(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = (ex.InnerException == null ? ex.Message : ex.InnerException.Message).Replace(Environment.NewLine, " ")
                });
                return BadRequest(respuesta);
            }
            
        }

        /// <summary>
        /// Se envia parametros mediante tipo multipart/form-data
        /// Se requiere el parametro IdUsuario:long    y    Archivo:IFormFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImagenMetodo3")]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseSubirImagenDto), 200)]
        public async Task<ActionResult<UsuarioResponseSubirImagenDto>> ImagenMetodo3()
        {
            UsuarioResponseSubirImagenDto respuesta = new UsuarioResponseSubirImagenDto();

            try
            {
                var archivoTemp = Request.Form.Files["Archivo"];
                var idUsuarioTemp = Request.Form["IdUsuario"];
            }
            catch (InvalidOperationException invEx)
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdUsuario' deben ser enviados mediante 'multipart/form-data'"
                });
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = (string.IsNullOrEmpty(invEx.StackTrace) ? invEx.Message : invEx.StackTrace).Replace(Environment.NewLine, " ")
                });
                return BadRequest(respuesta);
            }

            var archivo = Request.Form.Files["Archivo"];
            var idUsuario = Request.Form["IdUsuario"];
            
            if (archivo == null || string.IsNullOrEmpty(idUsuario))
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdUsuario' deben ser enviados mediante 'multipart/form-data'"
                });
                return BadRequest(respuesta);
            }

            //transformar IFormFile hacia bytes
            var file = archivo;
            if (file.Length == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            string urlImagenNueva = string.Empty;
            var nombreArchivo = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string extension = Path.GetExtension(nombreArchivo).Trim().Replace(".", string.Empty).ToLower();
            byte[] archivoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                archivoBytes = memoryStream.ToArray();
            }
            UsuarioModificarImagenMetodo1FiltroDto modelo = new UsuarioModificarImagenMetodo1FiltroDto
            {
                ArchivoBytes = archivoBytes,
                ExtensionSinPunto = extension,
                IdUsuario = Convert.ToInt64(idUsuario)
            };
            var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo, ref urlImagenNueva));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpDelete("Imagen/{id}")]
        [ProducesResponseType(typeof(UsuarioResponseEliminarImagenDto), 404)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarImagenDto), 400)]
        [ProducesResponseType(typeof(UsuarioResponseEliminarImagenDto), 200)]
        public async Task<ActionResult<UsuarioResponseEliminarImagenDto>> Imagen(long id)
        {
            string urlImagen = string.Empty;
            UsuarioResponseEliminarImagenDto respuesta = new UsuarioResponseEliminarImagenDto();
            var entidad = await Task.FromResult(_lnUsuario.EliminarImagen(id, ref urlImagen));
            if (entidad == -1)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            respuesta.UrlImagen = urlImagen;
            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }
    }
}
