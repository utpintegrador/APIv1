using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Configuracion.Proceso;
using Entidad.Response;
using Entidad.Dto.Maestro;
using Entidad.Response.Maestro;
using Entidad.Entidad.Maestro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly LnCategoria _lnCategoria = new LnCategoria();
        private readonly IMapper mapper;

        public CategoriaController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpPost("Obtener")]
        public async Task<ActionResult<CategoriaResponseObtenerDto>> Obtener([FromBody] CategoriaObtenerFiltroDto filtro)
        {
            CategoriaResponseObtenerDto respuesta = new CategoriaResponseObtenerDto();
            var result = await Task.FromResult(_lnCategoria.Obtener(filtro));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;

            if (result.Any())
            {
                respuesta.CantidadTotalRegistros = result.First().TotalItems;
            }

            return Ok(respuesta);
        }

        [HttpGet("{id}", Name = "ObtenerCategoriaPorId")]
        [ProducesResponseType(typeof(CategoriaResponseObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(CategoriaResponseObtenerPorIdDto), 200)]
        public async Task<ActionResult<CategoriaResponseObtenerPorIdDto>> ObtenerPorId(int id)
        {
            CategoriaResponseObtenerPorIdDto respuesta = new CategoriaResponseObtenerPorIdDto();
            var entidad = await Task.FromResult(_lnCategoria.ObtenerPorId(id));
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
        [ProducesResponseType(typeof(CategoriaResponseRegistrarDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseRegistrarDto), 200)]
        public async Task<ActionResult<CategoriaResponseRegistrarDto>> Registrar([FromBody] CategoriaRegistrarDto modelo)
        {
            CategoriaResponseRegistrarDto respuesta = new CategoriaResponseRegistrarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            int nuevoId = 0;
            var result = await Task.FromResult(_lnCategoria.Registrar(modelo, ref nuevoId));
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
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 404)]
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 200)]
        public async Task<ActionResult<CategoriaResponseModificarDto>> Modificar([FromBody] CategoriaModificarDto modelo)
        {
            CategoriaResponseModificarDto respuesta = new CategoriaResponseModificarDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

            var entidad = await Task.FromResult(_lnCategoria.ObtenerPorId(modelo.IdCategoria));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            var result = await Task.FromResult(_lnCategoria.Modificar(modelo));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar modificar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CategoriaResponseEliminarDto), 404)]
        [ProducesResponseType(typeof(CategoriaResponseEliminarDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseEliminarDto), 200)]
        public async Task<ActionResult<CategoriaResponseEliminarDto>> Eliminar(int id)
        {
            CategoriaResponseEliminarDto respuesta = new CategoriaResponseEliminarDto();
            var entidad = await Task.FromResult(_lnCategoria.ObtenerPorId(id));
            if (entidad == null)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

            int result = await Task.FromResult(_lnCategoria.Eliminar(id));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar el registro" });
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
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 200)]
        public async Task<ActionResult<CategoriaResponseSubirImagenDto>> ImagenMetodo1([FromBody] CategoriaModificarImagenMetodo1FiltroDto modelo)
        {
            string urlImagenNueva = string.Empty;

            CategoriaResponseSubirImagenDto respuesta = new CategoriaResponseSubirImagenDto();
            if (!ModelState.IsValid)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros enviados no son correctos" });
                return BadRequest(respuesta);
            }

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

            var result = await Task.FromResult(_lnCategoria.SubirImagenAws(modelo, ref urlImagenNueva));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }
            if (result == -1)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "El IdCategoria proporcionado no es válido" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.UrlImagen = urlImagenNueva;

            return Ok(respuesta);

        }

        [HttpPost("ImagenMetodo2")]
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 200)]
        public async Task<ActionResult<CategoriaResponseSubirImagenDto>> ImagenMetodo2(IFormFile archivo, long idCategoria)
        {
            CategoriaResponseSubirImagenDto respuesta = new CategoriaResponseSubirImagenDto();
            try
            {
                if (archivo == null || idCategoria == 0)
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
                CategoriaModificarImagenMetodo1FiltroDto modelo = new CategoriaModificarImagenMetodo1FiltroDto
                {
                    ArchivoBytes = archivoBytes,
                    ExtensionSinPunto = extension,
                    IdCategoria = idCategoria
                };
                var result = await Task.FromResult(_lnCategoria.SubirImagenAws(modelo, ref urlImagenNueva));
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
                    Mensaje = "Los parametros 'Archivo' y 'IdCategoria' deben ser enviados mediante 'multipart/form-data'"
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
        /// Se requiere el parametro IdCategoria:long    y    Archivo:IFormFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImagenMetodo3")]
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseSubirImagenDto), 200)]
        public async Task<ActionResult<CategoriaResponseSubirImagenDto>> ImagenMetodo3()
        {
            CategoriaResponseSubirImagenDto respuesta = new CategoriaResponseSubirImagenDto();

            try
            {
                var archivoTemp = Request.Form.Files["Archivo"];
                var idCategoriaTemp = Request.Form["IdCategoria"];
            }
            catch (InvalidOperationException invEx)
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdCategoria' deben ser enviados mediante 'multipart/form-data'"
                });
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = (string.IsNullOrEmpty(invEx.StackTrace) ? invEx.Message : invEx.StackTrace).Replace(Environment.NewLine, " ")
                });
                return BadRequest(respuesta);
            }

            var archivo = Request.Form.Files["Archivo"];
            var idCategoria = Request.Form["IdCategoria"];

            if (archivo == null || string.IsNullOrEmpty(idCategoria))
            {
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "Los parametros 'Archivo' y 'IdCategoria' deben ser enviados mediante 'multipart/form-data'"
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
            CategoriaModificarImagenMetodo1FiltroDto modelo = new CategoriaModificarImagenMetodo1FiltroDto
            {
                ArchivoBytes = archivoBytes,
                ExtensionSinPunto = extension,
                IdCategoria = Convert.ToInt64(idCategoria)
            };
            var result = await Task.FromResult(_lnCategoria.SubirImagenAws(modelo, ref urlImagenNueva));
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
        [ProducesResponseType(typeof(CategoriaResponseEliminarImagenDto), 404)]
        [ProducesResponseType(typeof(CategoriaResponseEliminarImagenDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseEliminarImagenDto), 200)]
        public async Task<ActionResult<CategoriaResponseEliminarImagenDto>> Imagen(long id)
        {
            string urlImagen = string.Empty;
            CategoriaResponseEliminarImagenDto respuesta = new CategoriaResponseEliminarImagenDto();
            var entidad = await Task.FromResult(_lnCategoria.EliminarImagen(id, ref urlImagen));
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
