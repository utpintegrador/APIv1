using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.CustomHandler;
using AutoMapper;
using Entidad.Request.Maestro;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoImagenController : ControllerBase
    {
        private readonly LnProductoImagen _lnProductoImagen = new LnProductoImagen();
        private readonly IMapper mapper;

        public ProductoImagenController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("ObtenerPorIdProducto")]
        [ProducesResponseType(typeof(ResponseProductoImagenObtenerPorIdProductoDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoImagenObtenerPorIdProductoDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoImagenObtenerPorIdProductoDto>> ObtenerPorIdNegocio([FromBody] RequestProductoImagenObtenerPorIdProductoDto filtro)
        {
            ResponseProductoImagenObtenerPorIdProductoDto respuesta = new ResponseProductoImagenObtenerPorIdProductoDto();
            var result = await Task.FromResult(_lnProductoImagen.ObtenerPorIdProducto(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        /// <summary>
        /// modelo.ArchivoBytes = byte[]
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("ImagenMetodo1")]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseProductoSubirImagenDto>> ImagenMetodo1([FromBody] RequestProductoImagenModificarImagenMetodo1Dto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            string urlImagenNueva = string.Empty;

            ResponseProductoSubirImagenDto respuesta = new ResponseProductoSubirImagenDto();

            if (modelo == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            if (modelo.ArchivoBytes == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El archivo de bytes es requerido" });
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnProductoImagen.SubirImagenAws(modelo, ref urlImagenNueva));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result == -1)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdProducto proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpPost("ImagenMetodo2")]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseProductoSubirImagenDto>> ImagenMetodo2(IFormFile archivo, long idProducto)
        {
            ResponseProductoSubirImagenDto respuesta = new ResponseProductoSubirImagenDto();
            try
            {
                if (archivo == null || idProducto == 0)
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
                RequestProductoImagenModificarImagenMetodo1Dto modelo = new RequestProductoImagenModificarImagenMetodo1Dto
                {
                    ArchivoBytes = archivoBytes,
                    ExtensionSinPunto = extension,
                    IdProducto = idProducto
                };
                var result = await Task.FromResult(_lnProductoImagen.SubirImagenAws(modelo, ref urlImagenNueva));
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
                    Mensaje = "Los parametros 'Archivo' y 'IdProducto' deben ser enviados mediante 'multipart/form-data'"
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
        /// Se requiere el parametro IdProducto:long    y    Archivo:IFormFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImagenMetodo3")]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseProductoSubirImagenDto>> ImagenMetodo3()
        {
            ResponseProductoSubirImagenDto respuesta = new ResponseProductoSubirImagenDto();

            try
            {
                var archivoTemp = Request.Form.Files["Archivo"];
                var idProductoTemp = Request.Form["IdProducto"];
            }
            catch (InvalidOperationException invEx)
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdProducto' deben ser enviados mediante 'multipart/form-data'"
                });
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = (string.IsNullOrEmpty(invEx.StackTrace) ? invEx.Message : invEx.StackTrace).Replace(Environment.NewLine, " ")
                });
                return BadRequest(respuesta);
            }

            var archivo = Request.Form.Files["Archivo"];
            var idProducto = Request.Form["IdProducto"];

            if (archivo == null || string.IsNullOrEmpty(idProducto))
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdProducto' deben ser enviados mediante 'multipart/form-data'"
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
            RequestProductoImagenModificarImagenMetodo1Dto modelo = new RequestProductoImagenModificarImagenMetodo1Dto
            {
                ArchivoBytes = archivoBytes,
                ExtensionSinPunto = extension,
                IdProducto = Convert.ToInt64(idProducto)
            };
            var result = await Task.FromResult(_lnProductoImagen.SubirImagenAws(modelo, ref urlImagenNueva));
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
        [ProducesResponseType(typeof(ResponseProductoEliminarImagenDto), 404)]
        [ProducesResponseType(typeof(ResponseProductoEliminarImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseProductoEliminarImagenDto), 200)]
        public async Task<ActionResult<ResponseProductoEliminarImagenDto>> Imagen(long id)
        {
            string urlImagen = string.Empty;
            ResponseProductoEliminarImagenDto respuesta = new ResponseProductoEliminarImagenDto();
            var entidad = await Task.FromResult(_lnProductoImagen.EliminarImagen(id, ref urlImagen));
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