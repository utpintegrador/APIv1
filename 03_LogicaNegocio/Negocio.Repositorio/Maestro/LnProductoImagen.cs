using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Datos.Repositorio.Maestro;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using Entidad.Vo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Negocio.Repositorio.Maestro
{
    public class LnProductoImagen: Logger
    {
        private readonly string _llaveAmazon = Entidad.Vo.ConstanteVo.AccessKeyAws;
        private readonly string _claveAmazon = Entidad.Vo.ConstanteVo.SecretAccessKeyAws;
        
        private readonly AdProductoImagen _adProductoImagen = new AdProductoImagen();

        //Obtener Imagen del Producto
        public List<ProductoImagenObtenerPorIdProductoDto> ObtenerPorIdProducto(RequestProductoImagenObtenerPorIdProductoDto filtro)
        {
            if (filtro == null) filtro = new RequestProductoImagenObtenerPorIdProductoDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "Predeterminado";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var lista = _adProductoImagen.ObtenerPorIdProducto(filtro);
            if (lista == null)
            {
                lista = new List<ProductoImagenObtenerPorIdProductoDto>();
            }
            return lista;
        }

        //Registrar Producto con Imagen
        public int Registrar(long idProducto, string url, ref long idNuevo)
        {
            return _adProductoImagen.Registrar(idProducto, url, ref idNuevo);
        }

        //Subir Imagen del Producto
        public int SubirImagenAws(RequestProductoImagenModificarImagenMetodo1Dto entidad, ref string url)
        {
            int respuesta = 0;
            try
            {
                url = ConstanteVo.UrlAmazon;
                string nombreDirectorio = "Producto";

                using (var client = new AmazonS3Client(
                    Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.AccessKeyAws),
                    Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.SecretAccessKeyAws),
                    RegionEndpoint.USEast2))
                {
                    string nombreArchivo = string.Format("{0}_{1}{2}{3}_{4}{5}{6}_{7}.{8}",
                            entidad.IdProducto,
                            DateTime.Now.Year.ToString("d4"),
                            DateTime.Now.Month.ToString("d2"),
                            DateTime.Now.Day.ToString("d2"),
                            DateTime.Now.Hour.ToString("d2"),
                            DateTime.Now.Minute.ToString("d2"),
                            DateTime.Now.Second.ToString("d2"),
                            DateTime.Now.Millisecond.ToString("d3"),
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

                        long idNuevo = 0;
                        respuesta = Registrar(entidad.IdProducto, url, ref idNuevo);
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

        //Eliminar Imagen del Producto
        public int EliminarImagen(long idProductoImagen)
        {
            int respuesta = 0;
            try
            {
                var objetoImagenBd = _adProductoImagen.ObtenerUrlImagenPorId(idProductoImagen);
                if (objetoImagenBd == null)
                {
                    respuesta = -1;
                }
                else
                {
                    int respuestaAws = EliminarImagenAws(objetoImagenBd.UrlImagen, idProductoImagen);
                    if (respuestaAws > 0)
                    {
                        respuesta = _adProductoImagen.Eliminar(idProductoImagen);
                    }
                }
            }
            catch
            {

            }
            return respuesta;
        }

    
        private int EliminarImagenAws(string urlImagenBd, long idProductoImagen)
        {
            int respuesta = 0;
            try
            {
                if (!string.IsNullOrEmpty(urlImagenBd))
                {
                    if (urlImagenBd != "https://encuentralo.s3.us-east-2.amazonaws.com/Aplicativo/producto_sin_imagen.jpg")
                    {
                        string nombreDirectorio = "Producto";

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
