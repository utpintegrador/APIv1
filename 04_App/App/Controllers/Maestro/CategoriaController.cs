using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Response;
using Entidad.Response.Maestro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Maestro;
using App.CustomHandler;
using Entidad.Request.Maestro;
using System.Collections.Generic;

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

        /// <param name="filtro">"NumeroPagina": 1, "CantidadRegistros": 10, "ColumnaOrden": "IdCategoria", "DireccionOrden": "desc"</param>
        /// <returns></returns>
        [HttpPost("Obtener")]
        public async Task<ActionResult<ResponseCategoriaObtenerDto>> Obtener([FromBody] RequestCategoriaObtenerDto filtro)
        {
            ResponseCategoriaObtenerDto respuesta = new ResponseCategoriaObtenerDto();
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
        [ProducesResponseType(typeof(ResponseCategoriaObtenerPorIdDto), 404)]
        [ProducesResponseType(typeof(ResponseCategoriaObtenerPorIdDto), 200)]
        public async Task<ActionResult<ResponseCategoriaObtenerPorIdDto>> ObtenerPorId(int id)
        {
            ResponseCategoriaObtenerPorIdDto respuesta = new ResponseCategoriaObtenerPorIdDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

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
        [ProducesResponseType(typeof(ResponseCategoriaRegistrarDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaRegistrarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseCategoriaRegistrarDto>> Registrar([FromBody] RequestCategoriaRegistrarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseCategoriaRegistrarDto respuesta = new ResponseCategoriaRegistrarDto();

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
        [ProducesResponseType(typeof(ResponseCategoriaModificarDto), 404)]
        [ProducesResponseType(typeof(ResponseCategoriaModificarDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaModificarDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseCategoriaModificarDto>> Modificar([FromBody] RequestCategoriaModificarDto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            ResponseCategoriaModificarDto respuesta = new ResponseCategoriaModificarDto();

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
        [ProducesResponseType(typeof(ResponseCategoriaEliminarDto), 404)]
        [ProducesResponseType(typeof(ResponseCategoriaEliminarDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaEliminarDto), 200)]
        public async Task<ActionResult<ResponseCategoriaEliminarDto>> Eliminar(int id)
        {
            ResponseCategoriaEliminarDto respuesta = new ResponseCategoriaEliminarDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

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
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 200)]
        [ValidationActionFilter]
        public async Task<ActionResult<ResponseCategoriaSubirImagenDto>> ImagenMetodo1([FromBody] RequestCategoriaModificarImagenMetodo1Dto modelo)
        {
            if (!ModelState.IsValid) return BadRequest();
            string urlImagenNueva = string.Empty;

            ResponseCategoriaSubirImagenDto respuesta = new ResponseCategoriaSubirImagenDto();

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
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseCategoriaSubirImagenDto>> ImagenMetodo2(IFormFile archivo, int idCategoria)
        {
            ResponseCategoriaSubirImagenDto respuesta = new ResponseCategoriaSubirImagenDto();
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
                RequestCategoriaModificarImagenMetodo1Dto modelo = new RequestCategoriaModificarImagenMetodo1Dto
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
        /// Se requiere el parametro IdCategoria:int    y    Archivo:IFormFile
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImagenMetodo3")]
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaSubirImagenDto), 200)]
        public async Task<ActionResult<ResponseCategoriaSubirImagenDto>> ImagenMetodo3()
        {
            ResponseCategoriaSubirImagenDto respuesta = new ResponseCategoriaSubirImagenDto();

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
            RequestCategoriaModificarImagenMetodo1Dto modelo = new RequestCategoriaModificarImagenMetodo1Dto
            {
                ArchivoBytes = archivoBytes,
                ExtensionSinPunto = extension,
                IdCategoria = Convert.ToInt32(idCategoria)
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
        [ProducesResponseType(typeof(ResponseCategoriaEliminarImagenDto), 404)]
        [ProducesResponseType(typeof(ResponseCategoriaEliminarImagenDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaEliminarImagenDto), 200)]
        public async Task<ActionResult<ResponseCategoriaEliminarImagenDto>> Imagen(long id)
        {
            string urlImagen = string.Empty;
            ResponseCategoriaEliminarImagenDto respuesta = new ResponseCategoriaEliminarImagenDto();
            if (id == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Objeto no encontrado con el ID proporcionado" });
                return NotFound(respuesta);
            }

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


        [HttpGet("ObtenerCombo/{idEstado}")]
        [ProducesResponseType(typeof(ResponseCategoriaObtenerComboDto), 200)]
        [ProducesResponseType(typeof(ResponseCategoriaObtenerComboDto), 400)]
        [ProducesResponseType(typeof(ResponseCategoriaObtenerComboDto), 404)]
        public async Task<ActionResult<ResponseCategoriaObtenerComboDto>> ObtenerCombo(int idEstado)
        {
            ResponseCategoriaObtenerComboDto respuesta = new ResponseCategoriaObtenerComboDto();
            if (idEstado == 0)
            {
                respuesta.ListaError = new List<ErrorDto>();
                respuesta.ListaError.Add(new ErrorDto
                {
                    Mensaje = "IdEstado: parametro es requerido"
                });
                respuesta.ProcesadoOk = 0;
                return BadRequest(respuesta);
            }

            var result = await Task.FromResult(_lnCategoria.ObtenerCombo(idEstado));
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
            return Ok(respuesta);
        }

    }
}
