using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Global;
using Entidad.Dto.Maestro;
using Entidad.Dto.Response.Maestro;
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

        [HttpGet]
        public async Task<ActionResult<CategoriaResponseObtenerDto>> Obtener()
        {
            CategoriaResponseObtenerDto respuesta = new CategoriaResponseObtenerDto();
            var result = await Task.FromResult(_lnCategoria.Obtener());
            respuesta.ProcesadoOk = 1;
            respuesta.Cuerpo = result;
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
        public async Task<ActionResult<CategoriaResponseRegistrarDto>> Registrar()//[FromBody] Models.ModelRegistrarCategoria modelo)
        {
            CategoriaResponseRegistrarDto respuesta = new CategoriaResponseRegistrarDto();

            var archivoRequest = Request.Form.Files["Archivo"];
            var accionRequest = Request.Form["Accion"];
            var descripcionRequest = Request.Form["Descripcion"];

            if (string.IsNullOrEmpty(accionRequest) || string.IsNullOrEmpty(descripcionRequest))
            {
                Logger.Log(Logger.Level.Error, "Los parametros de Accion y Descripcion son requeridos");
                //respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros de Accion y IdUsuario son requeridos" });
                return BadRequest(respuesta);
            }


            try
            {
                switch (accionRequest.ToString().Trim())
                {
                    //case "del":
                    //    {
                    //        AwsS3EliminarUsuarioDto prm = new AwsS3EliminarUsuarioDto();
                    //        prm.IdUsuario = Convert.ToInt64(idUsuarioRequest.ToString());
                    //        var result = await Task.FromResult(_lnS3Service.EliminarImagenUsuarioAwsS3(prm));
                    //        if (result == 0)
                    //        {
                    //            //respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar" });
                    //            return BadRequest(respuesta);
                    //        }

                    //        respuesta.ProcesadoOk = 1;

                    //        return Ok(respuesta);
                    //    }
                    case "add":
                        {
                            var file = archivoRequest; //Request.Form.Files[0];
                            if (file == null)
                            {
                                Logger.Log(Logger.Level.Error, "No se ha proporcionado un archivo de tipo imágen");
                                //respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere un archivo tipo imagen" });
                                return BadRequest(respuesta);
                            }
                            else if (file.Length > 0)
                            {
                                //var nombreArchivo = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                var nombreArchivo = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                //string nombreArchivo = file.FileName;
                                string extension = Path.GetExtension(nombreArchivo).Trim().Replace(".", string.Empty).ToLower();

                                if (extension.Equals("jpg") || extension.Equals("png") || extension.Equals("jpeg") || extension.Equals("bmp") || extension.Equals("gif"))
                                {
                                    byte[] archivo;
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        archivo = memoryStream.ToArray();
                                    }

                                    int nuevoId = 0;
                                    string url = string.Empty;
                                    if (archivo != null)
                                    {
                                        CategoriaRegistrarDto prm = new CategoriaRegistrarDto();
                                        prm.Archivo = archivo;
                                        prm.ExtensionSinPunto = extension;
                                        prm.Descripcion = descripcionRequest.ToString();
                                        var result = await Task.FromResult(_lnCategoria.Registrar(prm, ref nuevoId, ref url));
                                        if (result == 0)
                                        {
                                            //respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                                            return BadRequest(respuesta);
                                        }

                                        respuesta.ProcesadoOk = 1;
                                        respuesta.IdGenerado = nuevoId;
                                        respuesta.UrlImagen = url;
                                    }

                                    return Ok(respuesta);
                                }
                                else
                                {
                                    Logger.Log(Logger.Level.Error, "Solo se aceptan imagenes jpg, png, jpeg, gif y bmp");
                                    //respuesta.ListaError.Add(new ErrorDto { Mensaje = "Solo se aceptan imagenes jpg, png, jpeg, gif y bmp" });
                                    return BadRequest(respuesta);
                                }

                            }
                            break;
                        }
                    default:
                        Logger.Log(Logger.Level.Error, "El parametro Accion solo debe contener los valores de 'add' o 'del'");
                        break;
                }

            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                //respuesta.ListaError.Add(new ErrorDto { Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }

            return BadRequest(respuesta);

        }

        [HttpPut()]//"{id}")]
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 404)]
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 400)]
        [ProducesResponseType(typeof(CategoriaResponseModificarDto), 200)]
        public async Task<ActionResult<CategoriaResponseModificarDto>> Modificar([FromBody] Categoria modelo)
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
    }
}
