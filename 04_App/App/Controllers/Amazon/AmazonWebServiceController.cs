using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using App.Models;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Amazon;
using Entidad.Response;
using Entidad.Response.Amazon;
using Microsoft.AspNetCore.Mvc;
using Negocio.Repositorio.Amazon;

namespace App.Controllers.Amazon
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AmazonWebServiceController : ControllerBase
    {
        private readonly LnS3Service _lnS3Service = new LnS3Service();

        [HttpGet("ObtenerImagenAlbum/{idUsuario}/{idAlbum}")]
        public async Task<ActionResult<AwsResponseObtenerDto>> ObtenerImagenAlbum(long idUsuario, long idAlbum)
        {
            AwsResponseObtenerDto respuesta = new AwsResponseObtenerDto();
            var result = await Task.FromResult(_lnS3Service.ObtenerImagenAlbum(idUsuario, idAlbum));
            respuesta.ProcesadoOk = 1;
            respuesta.ListaError = new List<ErrorDto>();
            respuesta.Cuerpo = result;

            return respuesta;
        }

        [HttpGet("ObtenerImagenGaleria/{idUsuario}/{idAlbum}/{idImagen}")]
        public async Task<ActionResult<AwsResponseObtenerDto>> ObtenerImagenGaleria(long idUsuario, long idAlbum, long idImagen)
        {
            AwsResponseObtenerDto respuesta = new AwsResponseObtenerDto();
            var result = await Task.FromResult(_lnS3Service.ObtenerImagenGaleria(idUsuario, idAlbum, idImagen));
            respuesta.ProcesadoOk = 1;
            respuesta.ListaError = new List<ErrorDto>();
            respuesta.Cuerpo = result;

            return respuesta;
        }

        [HttpGet("ObtenerImagenUsuario/{idUsuario}")]
        public async Task<ActionResult<AwsResponseObtenerDto>> ObtenerImagenUsuario(long idUsuario)
        {
            AwsResponseObtenerDto respuesta = new AwsResponseObtenerDto();
            var result = await Task.FromResult(_lnS3Service.ObtenerImagenUsuario(idUsuario));
            respuesta.ProcesadoOk = 1;
            respuesta.ListaError = new List<ErrorDto>();
            respuesta.Cuerpo = result;

            return respuesta;
        }

        [HttpPost("SubirImagenAlbum")]
        public async Task<ActionResult<AwsResponseRegistrarDto>> SubirImagenAlbum([FromBody] AwsS3RegistrarAlbumDto entidad)
        {
            AwsResponseRegistrarDto respuesta = new AwsResponseRegistrarDto();

            long nuevoId = 0;
            string url = string.Empty;
            var result = await Task.FromResult(_lnS3Service.SubirImagenAlbumAwsS3(entidad, ref nuevoId, ref url));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;
            respuesta.UrlImagen = url;

            return Ok(respuesta);
        }

        [HttpPost("SubirImagenGaleria")]
        public async Task<ActionResult<AwsResponseRegistrarDto>> SubirImagenGaleria([FromBody] AwsS3RegistrarGaleriaDto entidad)
        {
            AwsResponseRegistrarDto respuesta = new AwsResponseRegistrarDto();

            long nuevoId = 0;
            string url = string.Empty;
            var result = await Task.FromResult(_lnS3Service.SubirImagenGaleriaAwsS3(entidad, ref nuevoId, ref url));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;
            respuesta.IdGenerado = nuevoId;
            respuesta.UrlImagen = url;

            return Ok(respuesta);
        }

        //[HttpPost("SubirImagenPerfil")]
        //public async Task<ActionResult<AwsResponseRegistrarDto>> SubirImagenPerfil([FromBody] AwsS3RegistrarPerfilDto entidad)
        //{
        //    AwsResponseRegistrarDto respuesta = new AwsResponseRegistrarDto();

        //    long nuevoId = 0;
        //    string url = string.Empty;
        //    var result = await Task.FromResult(_lnS3Service.SubirImagenPerfilAwsS3(entidad, ref nuevoId, ref url));
        //    if (result == 0)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
        //        return BadRequest(respuesta);
        //    }

        //    respuesta.ProcesadoOk = 1;
        //    respuesta.IdGenerado = nuevoId;
        //    respuesta.UrlImagen = url;

        //    return Ok(respuesta);
        //}

        //Invocarlo como multipart/form-data
        [HttpPost("SubirImagenUsuario")]//, DisableRequestSizeLimit]
        public async Task<ActionResult<AwsResponseRegistrarDto>> SubirImagenUsuario()
        {
            AwsResponseRegistrarDto respuesta = new AwsResponseRegistrarDto();

            var archivoRequest = Request.Form.Files["Archivo"];
            var accionRequest = Request.Form["Accion"];
            var idUsuarioRequest = Request.Form["IdUsuario"];

            if(string.IsNullOrEmpty(accionRequest) || string.IsNullOrEmpty(idUsuarioRequest))
            {
                Logger.Log(Logger.Level.Error, "Los parametros de Accion y IdUsuario son requeridos");
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Los parametros de Accion y IdUsuario son requeridos" });
                return BadRequest(respuesta);
            }

            
            try
            {
                switch (accionRequest.ToString().Trim())
                {
                    case "del":
                        {
                            AwsS3EliminarUsuarioDto prm = new AwsS3EliminarUsuarioDto();
                            prm.IdUsuario = Convert.ToInt64(idUsuarioRequest.ToString());
                            var result = await Task.FromResult(_lnS3Service.EliminarImagenUsuarioAwsS3(prm));
                            if (result == 0)
                            {
                                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar" });
                                return BadRequest(respuesta);
                            }

                            respuesta.ProcesadoOk = 1;

                            return Ok(respuesta);
                        }
                    case "add":
                        {
                            var file = archivoRequest; //Request.Form.Files[0];
                            if (file == null)
                            {
                                Logger.Log(Logger.Level.Error, "No se ha proporcionado un archivo de tipo imágen");
                                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere un archivo tipo imagen" });
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

                                    long nuevoId = 0;
                                    string url = string.Empty;
                                    if (archivo != null)
                                    {
                                        AwsS3RegistrarUsuarioDto prm = new AwsS3RegistrarUsuarioDto();
                                        prm.Archivo = archivo;
                                        prm.ExtensionSinPunto = extension;
                                        prm.IdUsuario = Convert.ToInt64(idUsuarioRequest.ToString());
                                        var result = await Task.FromResult(_lnS3Service.SubirImagenUsuarioAwsS3(prm, ref nuevoId, ref url));
                                        if (result == 0)
                                        {
                                            respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
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
                                    respuesta.ListaError.Add(new ErrorDto { Mensaje = "Solo se aceptan imagenes jpg, png, jpeg, gif y bmp" });
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
                respuesta.ListaError.Add(new ErrorDto { Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }

            return BadRequest(respuesta);
        }

        //[HttpPost("SubirImagenUsuario2")]//, DisableRequestSizeLimit]
        //public async Task<ActionResult<AwsResponseRegistrarDto>> SubirImagenUsuario2()//[FromForm] ModelAwsS3RegistrarUsuario2 entidad)
        //{

        //    var archivoRequest = Request.Form.Files["Archivo"];
        //    var idUsuarioRequest = Request.Form["IdUsuario"];

        //    if(archivoRequest != null)
        //    {
        //        Logger.Log(Logger.Level.Info, archivoRequest.FileName);
        //    }
        //    if(idUsuarioRequest.Count>0)
        //    {
        //        Logger.Log(Logger.Level.Info, idUsuarioRequest.ToString());
        //    }

        //    //String json = Newtonsoft.Json.JsonConvert.SerializeObject(Request.Form.Keys);
        //    //Logger.Log(Logger.Level.Info, json);

        //    AwsResponseRegistrarDto respuesta = new AwsResponseRegistrarDto();
        //    try
        //    {
        //        var file = archivoRequest; //Request.Form.Files[0];
        //        if (file == null)
        //        {
        //            respuesta.ListaError.Add(new ErrorDto { Mensaje = "Se requiere un archivo tipo imagen" });
        //            return BadRequest(respuesta);
        //        }
        //        else if (file.Length > 0)
        //        {
        //            var nombreArchivo = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            Logger.Log(Logger.Level.Info, nombreArchivo);
        //            //string nombreArchivo = file.FileName;
        //            string extension = Path.GetExtension(nombreArchivo).Trim().Replace(".", string.Empty).ToLower();
        //            Logger.Log(Logger.Level.Info, extension);

        //            if (extension.Equals("jpg") || extension.Equals("png") || extension.Equals("jpeg") || extension.Equals("bmp"))
        //            {
        //                byte[] archivo;
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await file.CopyToAsync(memoryStream);
        //                    archivo = memoryStream.ToArray();
        //                }

        //                long nuevoId = 0;
        //                string url = string.Empty;
        //                if (archivo != null)
        //                {
        //                    AwsS3RegistrarUsuarioDto prm = new AwsS3RegistrarUsuarioDto();
        //                    prm.Archivo = archivo;
        //                    prm.ExtensionSinPunto = extension;
        //                    prm.IdUsuario = 71;
        //                    var result = await Task.FromResult(_lnS3Service.SubirImagenUsuarioAwsS3(prm, ref nuevoId, ref url));
        //                    if (result == 0)
        //                    {
        //                        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar registrar" });
        //                        return BadRequest(respuesta);
        //                    }

        //                    respuesta.ProcesadoOk = 1;
        //                    respuesta.IdGenerado = nuevoId;
        //                    respuesta.UrlImagen = url;
        //                }

        //                return Ok(respuesta);
        //            }
        //            else
        //            {
        //                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Solo se aceptan imagenes jpg, png, jpeg y bmp" });
        //                return BadRequest(respuesta);
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(Logger.Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
        //    }

        //    return BadRequest(respuesta);
        //}

        [HttpDelete("EliminarImagenAlbum")]
        public async Task<ActionResult<AwsResponseEliminarDto>> EliminarImagenAlbum([FromBody] AwsS3EliminarAlbumDto entidad)
        {
            AwsResponseEliminarDto respuesta = new AwsResponseEliminarDto();

            //long nuevoId = 0;
            string url = string.Empty;
            var result = await Task.FromResult(_lnS3Service.EliminarAlbumAwsS3(entidad));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;

            return Ok(respuesta);
        }

        [HttpDelete("EliminarImagenGaleria")]
        public async Task<ActionResult<AwsResponseEliminarDto>> EliminarImagenGaleria([FromBody] AwsS3EliminarGaleriaDto entidad)
        {
            AwsResponseEliminarDto respuesta = new AwsResponseEliminarDto();

            //long nuevoId = 0;
            string url = string.Empty;
            var result = await Task.FromResult(_lnS3Service.EliminarImagenGaleriaAwsS3(entidad));
            if (result == 0)
            {
                respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar" });
                return BadRequest(respuesta);
            }

            respuesta.ProcesadoOk = 1;

            return Ok(respuesta);
        }

        //[HttpDelete("EliminarImagenUsuario")]
        //public async Task<ActionResult<AwsResponseEliminarDto>> EliminarImagenUsuario([FromBody] AwsS3EliminarUsuarioDto entidad)
        //{
        //    AwsResponseEliminarDto respuesta = new AwsResponseEliminarDto();

        //    var result = await Task.FromResult(_lnS3Service.EliminarImagenUsuarioAwsS3(entidad));
        //    if (result == 0)
        //    {
        //        respuesta.ListaError.Add(new ErrorDto { Mensaje = "Error al intentar eliminar" });
        //        return BadRequest(respuesta);
        //    }

        //    respuesta.ProcesadoOk = 1;

        //    return Ok(respuesta);
        //}
    }
}
