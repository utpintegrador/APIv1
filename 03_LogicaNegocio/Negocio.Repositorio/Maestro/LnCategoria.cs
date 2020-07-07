using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Datos.Repositorio.Maestro;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using Entidad.Request.Maestro;
using Entidad.Vo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Negocio.Repositorio.Maestro
{
    public class LnCategoria: Logger
    {
        private readonly string _llaveAmazon = Entidad.Vo.ConstanteVo.AccessKeyAws;
        private readonly string _claveAmazon = Entidad.Vo.ConstanteVo.SecretAccessKeyAws;
        //private readonly string _urlAmazon = "https://encuentralo.s3.us-east-2.amazonaws.com/";

        private readonly AdCategoria _adCategoria = new AdCategoria();

        public List<CategoriaObtenerDto> Obtener(RequestCategoriaObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestCategoriaObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "Descripcion";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "asc";
            var listado = _adCategoria.Obtener(filtro);
            if(listado == null)
            {
                listado = new List<CategoriaObtenerDto>();
            }
            return listado;
        }

        public Categoria ObtenerPorId(int id)
        {
            return _adCategoria.ObtenerPorId(id);
        }

        public int Registrar(RequestCategoriaRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            resultado = _adCategoria.Registrar(modelo, ref idNuevo);
            return resultado;
        }

        public int Modificar(RequestCategoriaModificarDto modelo)
        {
            if(modelo.IdEstado > 2 || modelo.IdEstado < 1)
            {
                modelo.IdEstado = 2;
            }
            return _adCategoria.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adCategoria.Eliminar(id);
        }

        public int ModificarUrlImagenPorIdCategoria(long idCategoria, string url)
        {
            return _adCategoria.ModificarUrlImagenPorIdCategoria(idCategoria, url);
        }

        public int SubirImagenAws(RequestCategoriaModificarImagenMetodo1Dto entidad, ref string url)
        {
            int respuesta = 0;
            try
            {
                var objetoImagenBd = _adCategoria.ObtenerUrlImagenPorId(entidad.IdCategoria);
                if (objetoImagenBd == null)
                {
                    url = string.Empty;
                    return -1;
                }

                url = ConstanteVo.UrlAmazon;
                string nombreDirectorio = "Categoria";

                int respuestaEliminar = EliminarImagenAws(objetoImagenBd.UrlImagen, entidad.IdCategoria);
                if (respuestaEliminar > 0)
                {
                    using (var client = new AmazonS3Client(
                        Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.AccessKeyAws),
                        Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.SecretAccessKeyAws),
                        RegionEndpoint.USEast2))
                    {
                        string nombreArchivo = string.Format("{0}.{1}",
                                entidad.IdCategoria,
                                entidad.ExtensionSinPunto);
                        url = string.Format("{0}{1}/{2}", url, nombreDirectorio, nombreArchivo);

                        using (var ms = new MemoryStream(entidad.ArchivoBytes))
                        {
                            var uploadRequest = new TransferUtilityUploadRequest
                            {
                                InputStream = ms,
                                Key = nombreArchivo,
                                BucketName = string.Format("encuentralo/{0}", nombreDirectorio),
                                CannedACL = S3CannedACL.PublicRead
                            };

                            var fileTransferUtility = new TransferUtility(client);
                            fileTransferUtility.Upload(uploadRequest);

                            //LnCategoria lnCategoria = new LnCategoria();
                            respuesta = ModificarUrlImagenPorIdCategoria(entidad.IdCategoria, url);
                        }
                    }
                }
            }
            catch (AmazonS3Exception exSe)
            {
                Log(Level.Error, String.Format("AmazonS3Exception: {0}", exSe));
            }
            catch (Exception ex)
            {
                Log(Level.Error, String.Format("Exception: {0}", ex));
            }

            return respuesta;
        }

        public int EliminarImagen(long idCategoria, ref string urlImagen)
        {
            int respuesta = 0;
            try
            {
                var objetoImagenBd = _adCategoria.ObtenerUrlImagenPorId(idCategoria);
                if (objetoImagenBd == null)
                {
                    urlImagen = string.Empty;
                    respuesta = -1;
                }
                else
                {
                    respuesta = EliminarImagenAws(objetoImagenBd.UrlImagen, idCategoria);
                    int respuestaModificarImagenBd = _adCategoria.EliminarUrlImagen(idCategoria);
                    if (respuestaModificarImagenBd > 0)
                    {
                        urlImagen = "https://encuentralo.s3.us-east-2.amazonaws.com/Aplicativo/sin_foto_perfil.jpg";
                    }
                }
            }
            catch
            {

            }
            return respuesta;
        }

        private int EliminarImagenAws(string urlImagenBd, long idCategoria)
        {
            int respuesta = 0;
            try
            {
                if (!string.IsNullOrEmpty(urlImagenBd))
                {
                    if (urlImagenBd != "https://encuentralo.s3.us-east-2.amazonaws.com/Aplicativo/sin_foto_perfil.jpg")
                    {
                        string nombreDirectorio = "Categoria";

                        string url = string.Format("{0}{1}/", ConstanteVo.UrlAmazon, nombreDirectorio);
                        string nombreArchivo = urlImagenBd.Replace(url, string.Empty);

                        var deleteObjectRequest = new DeleteObjectRequest
                        {
                            Key = nombreArchivo,
                            BucketName = string.Format("encuentralo/{0}", nombreDirectorio)
                        };

                        using (var client = new AmazonS3Client(
                            Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.AccessKeyAws),
                            Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.SecretAccessKeyAws),
                            RegionEndpoint.USEast2))
                        {
                            Task eliminar = Task.Run(() =>
                            {
                                client.DeleteObjectAsync(deleteObjectRequest);
                            });

                            eliminar.Wait();
                            if (eliminar.IsCompleted)
                            {
                                respuesta = 1;
                            }
                        }
                    }
                    else
                    {
                        respuesta = 1;
                    }
                }
                else
                {
                    respuesta = 1;
                }
            }
            catch (AmazonS3Exception exSe)
            {
                Log(Level.Error, String.Format("AmazonS3Exception: {0}", exSe));
            }
            catch (Exception ex)
            {
                Log(Level.Error, String.Format("Exception: {0}", ex));
            }

            return respuesta;
        }
    }
}
