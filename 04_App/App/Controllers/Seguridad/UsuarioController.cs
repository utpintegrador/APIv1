using System;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Seguridad;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using App.CustomHandler;
using Entidad.Request.Seguridad;
using Entidad.Configuracion.Proceso;

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
        public async Task<ActionResult<ResponseUsuarioObtenerDto>> Obtener([FromBody] RequestUsuarioObtenerDto filtro)
        {
            ResponseUsuarioObtenerDto respuesta = new ResponseUsuarioObtenerDto();
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
        [ProducesResponseType(typeof(ResponseUsuarioObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseUsuarioObtenerPorIdDto>> ObtenerPorId(long id)
        {
            ResponseUsuarioObtenerPorIdDto respuesta = new ResponseUsuarioObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

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
        [ProducesResponseType(typeof(ResponseUsuarioEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioEliminarDto), 200)]
        public async Task<ActionResult<ResponseUsuarioEliminarDto>> Eliminar(long id)
        {
            ResponseUsuarioEliminarDto respuesta = new ResponseUsuarioEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

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
        [ProducesResponseType(typeof(ResponseUsuarioRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseUsuarioRegistrarDto>> Registrar([FromBody] RequestUsuarioRegistrarDto modelo)
        {
            ResponseUsuarioRegistrarDto respuesta = new ResponseUsuarioRegistrarDto();
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
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 200)]
        public async Task<ActionResult<ResponseUsuarioModificarDto>> Modificar([FromBody] RequestUsuarioModificarDto modelo)
        {
            ResponseUsuarioModificarDto respuesta = new ResponseUsuarioModificarDto();
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


        [HttpPut("ModificarModoAdmin")]
        [ProducesResponseType(typeof(ResponseUsuarioModificarModoAdminDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarModoAdminDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarModoAdminDto), 200)]
        public async Task<ActionResult<ResponseUsuarioModificarModoAdminDto>> ModificarModoAdmin([FromBody] RequestUsuarioModificarModoAdminDto modelo)
        {
            ResponseUsuarioModificarModoAdminDto respuesta = new ResponseUsuarioModificarModoAdminDto();
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

            var result = await Task.FromResult(_lnUsuario.ModificarModoAdmin(modelo));
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
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioModificarDto), 200)]
        public async Task<ActionResult<ResponseUsuarioModificarDto>> ModificarContrasenia([FromBody] RequestUsuarioCambioContraseniaDto modelo)
        {
            ResponseUsuarioModificarDto respuesta = new ResponseUsuarioModificarDto();
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
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseUsuarioSubirImagenDto>> ImagenMetodo1([FromBody] RequestUsuarioModificarImagenMetodo1Dto modelo)
        {
            string urlImagenNueva = string.Empty;

            ResponseUsuarioSubirImagenDto respuesta = new ResponseUsuarioSubirImagenDto();
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

            var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo));//, ref urlImagenNueva));
            if (string.IsNullOrEmpty(result.Result))// == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result.Result == "0")
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result.Result == "-1")
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdUsuario proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = result.Result; //urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpPost("ImagenMetodo2")]
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseUsuarioSubirImagenDto>> ImagenMetodo2(IFormFile archivo, long idUsuario)
        {
            ResponseUsuarioSubirImagenDto respuesta = new ResponseUsuarioSubirImagenDto();
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
                RequestUsuarioModificarImagenMetodo1Dto modelo = new RequestUsuarioModificarImagenMetodo1Dto
                {
                    ArchivoBytes = archivoBytes,
                    ExtensionSinPunto = extension,
                    IdUsuario = idUsuario
                };
                var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo));//, ref urlImagenNueva));
                if (string.IsNullOrEmpty(result.Result))// == 0)
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                    return BadRequest(respuesta);
                }
                if (result.Result == "0")
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                    return BadRequest(respuesta);
                }
                if (result.Result == "-1")
                {
                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdUsuario proporcionado no es válido" });
                    return BadRequest(respuesta);
                }

                respuesta.ProcesadoOk = 1;
                respuesta.UrlImagen = result.Result; //urlImagenNueva;

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
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseUsuarioSubirImagenDto>> ImagenMetodo3()
        {
            ResponseUsuarioSubirImagenDto respuesta = new ResponseUsuarioSubirImagenDto();

            try
            {
                var archivoTemp = Request.Form.Files["Archivo"];
                var idUsuarioTemp = Request.Form["IdUsuario"];

                if(archivoTemp == null)
                {
                    Logger.Log(Logger.Level.Error, "No se obtuvo el valor del objeto Archivo");
                }

                if (string.IsNullOrEmpty(idUsuarioTemp))
                {
                    Logger.Log(Logger.Level.Error, "No se obtuvo el valor del objeto IdUsuario");
                }
            }
            catch (InvalidOperationException invEx)
            {
                Logger.Log(Logger.Level.Error, invEx.InnerException == null ? invEx.Message : invEx.InnerException.Message);
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
            RequestUsuarioModificarImagenMetodo1Dto modelo = new RequestUsuarioModificarImagenMetodo1Dto
            {
                ArchivoBytes = archivoBytes,
                ExtensionSinPunto = extension,
                IdUsuario = Convert.ToInt64(idUsuario)
            };
            var result = await Task.FromResult(_lnUsuario.SubirImagenAws(modelo));//, ref urlImagenNueva));
            if (string.IsNullOrEmpty(result.Result))// == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result.Result == "0")
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result.Result == "-1")
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdUsuario proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = result.Result; //urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpDelete("Imagen/{id}")]
        [ProducesResponseType(typeof(ResponseUsuarioEliminarImagenDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioEliminarImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioEliminarImagenDto), 200)]
        public async Task<ActionResult<ResponseUsuarioEliminarImagenDto>> Imagen(long id)
        {
            string urlImagen = string.Empty;
            ResponseUsuarioEliminarImagenDto respuesta = new ResponseUsuarioEliminarImagenDto();
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

        [HttpGet("ObtenerCombo/{idEstado}")]
        [ProducesResponseType(typeof(ResponseUsuarioObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseUsuarioObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseUsuarioObtenerComboDto>> ObtenerCombo(int idEstado)
        {
            ResponseUsuarioObtenerComboDto respuesta = new ResponseUsuarioObtenerComboDto();

            var result = await Task.FromResult(_lnUsuario.ObtenerCombo(idEstado));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

        [HttpGet("ObtenerContraseniaPorId/{id}")]
        [ProducesResponseType(typeof(ResponseUsuarioObtenerContraseniaPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseUsuarioObtenerContraseniaPorIdDto), 200)]
        public async Task<ActionResult<ResponseUsuarioObtenerContraseniaPorIdDto>> ObtenerContraseniaPorId(long id)
        {
            ResponseUsuarioObtenerContraseniaPorIdDto respuesta = new ResponseUsuarioObtenerContraseniaPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var entidad = await Task.FromResult(_lnUsuario.ObtenerContraseniaPorId(id));
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
