using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Amazon;
using Negocio.Repositorio.Perfil;
using Negocio.Repositorio.Seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Negocio.Repositorio.Amazon
{
    public class LnS3Service: Logger
    {
        private readonly string _llaveAmazon = "AKIA2F7OY6EWJJ46KFVY";
        private readonly string _claveAmazon = "EncdXICIiJOcFgwB1u2ISQt5s0mOr8nTZAO0RiMu";
        private readonly string _urlAmazon = "https://red-social.s3.us-east-2.amazonaws.com/";
        //private readonly string _urlImagenSinFoto = "https://red-social.s3.us-east-2.amazonaws.com/Aplicativo/sin_foto_perfil.jpg";

        public int SubirImagenAlbumAwsS3(AwsS3RegistrarAlbumDto entidad, ref long nuevoId, ref string url)
        {
            int respuesta = 0;
            try
            {
                url = _urlAmazon;
                string nombreDirectorio = "Album";

                //string rutaAchivo = @"C:\TCI\STACE_GENERAMENSAJES\Log\17\10\19\StaceAg_20171019.log";

                byte[] bytes = Convert.FromBase64String(entidad.ImagenStringBase64); //Encoding.ASCII.GetBytes(entidad.Imagen);

                using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                {
                    //FileStream fs = new FileStream(rutaAchivo, FileMode.Open, FileAccess.Read);
                    using (var ms = new MemoryStream(bytes))
                    {
                        //fs.CopyTo(ms);
                        string nombreArchivo = string.Format("{0}_{1}_{2}{3}{4}_{5}{6}{7}.{8}",
                            entidad.IdUsuario,
                            entidad.IdAlbum,
                            DateTime.Now.Year.ToString("d4"),
                            DateTime.Now.Month.ToString("d2"),
                            DateTime.Now.Day.ToString("d2"),
                            DateTime.Now.Hour.ToString("d2"),
                            DateTime.Now.Minute.ToString("d2"),
                            DateTime.Now.Second.ToString("d2"),
                            entidad.ExtensionSinPunto);

                        url = string.Format("{0}{1}/{2}", url, nombreDirectorio, nombreArchivo);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = ms,
                            Key = nombreArchivo,  //Path.GetFileName(rutaAchivo),
                            BucketName = string.Format("red-social/{0}", nombreDirectorio),
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        //await fileTransferUtility.UploadAsync(uploadRequest);
                        fileTransferUtility.Upload(uploadRequest);

                        LnAlbumImagen lnAlbumImagen = new LnAlbumImagen();
                        respuesta = lnAlbumImagen.ModificarImagen(new Entidad.Entidad.Perfil.AlbumImagen
                        {
                            IdAlbumImagen = entidad.IdAlbum,
                            UrlImagenAlbum = url
                        });

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

        public int SubirImagenGaleriaAwsS3(AwsS3RegistrarGaleriaDto entidad, ref long nuevoId, ref string url)
        {
            int respuesta = 0;
            try
            {
                url = _urlAmazon;
                string nombreDirectorio = "Imagen";

                //string rutaAchivo = @"C:\TCI\STACE_GENERAMENSAJES\Log\17\10\19\StaceAg_20171019.log";

                byte[] bytes = Convert.FromBase64String(entidad.ImagenStringBase64); //Encoding.ASCII.GetBytes(entidad.Imagen);

                using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                {
                    //FileStream fs = new FileStream(rutaAchivo, FileMode.Open, FileAccess.Read);
                    using (var ms = new MemoryStream(bytes))
                    {
                        //fs.CopyTo(ms);
                        string nombreArchivo = string.Format("{0}_{1}_{2}_{3}{4}{5}_{6}{7}{8}.{9}",
                            entidad.IdUsuario,
                            entidad.IdAlbum,
                            entidad.IdImagen,
                            DateTime.Now.Year.ToString("d4"),
                            DateTime.Now.Month.ToString("d2"),
                            DateTime.Now.Day.ToString("d2"),
                            DateTime.Now.Hour.ToString("d2"),
                            DateTime.Now.Minute.ToString("d2"),
                            DateTime.Now.Second.ToString("d2"),
                            entidad.ExtensionSinPunto);

                        url = string.Format("{0}{1}/{2}", url, nombreDirectorio, nombreArchivo);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = ms,
                            Key = nombreArchivo,  //Path.GetFileName(rutaAchivo),
                            BucketName = string.Format("red-social/{0}", nombreDirectorio),
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        //await fileTransferUtility.UploadAsync(uploadRequest);
                        fileTransferUtility.Upload(uploadRequest);

                        LnImagen lnImagen = new LnImagen();
                        respuesta = lnImagen.Registrar(new Entidad.Dto.Perfil.ImagenRegistrarDto
                        {
                            IdAlbumImagen = entidad.IdAlbum,
                            Url = url
                        }, ref nuevoId);
                        //respuesta = ProcesarImagen(tipo, Convert.ToInt64(entidad.IdUsuario), url);

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

        //public int SubirImagenPerfilAwsS3(AwsS3RegistrarPerfilDto entidad, ref long nuevoId, ref string url)
        //{
        //    int respuesta = 0;
        //    try
        //    {
        //        url = _urlAmazon;
        //        string nombreDirectorio = "Usuario";
        //        nuevoId = Convert.ToInt64(entidad.IdUsuario);

        //        //string rutaAchivo = @"C:\TCI\STACE_GENERAMENSAJES\Log\17\10\19\StaceAg_20171019.log";

        //        byte[] bytes = Convert.FromBase64String(entidad.ImagenStringBase64); //Encoding.ASCII.GetBytes(entidad.Imagen);

        //        using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
        //        {
        //            //FileStream fs = new FileStream(rutaAchivo, FileMode.Open, FileAccess.Read);
        //            using (var ms = new MemoryStream(bytes))
        //            {
        //                //fs.CopyTo(ms);
        //                string nombreArchivo = string.Format("{0}_{1}_{2}{3}{4}_{5}{6}{7}.{8}",
        //                    entidad.IdUsuario,
        //                    entidad.IdPerfil,
        //                    DateTime.Now.Year.ToString("d4"),
        //                    DateTime.Now.Month.ToString("d2"),
        //                    DateTime.Now.Day.ToString("d2"),
        //                    DateTime.Now.Hour.ToString("d2"),
        //                    DateTime.Now.Minute.ToString("d2"),
        //                    DateTime.Now.Second.ToString("d2"),
        //                    entidad.ExtensionSinPunto);

        //                url = string.Format("{0}{1}/{2}", url, nombreDirectorio, nombreArchivo);

        //                var uploadRequest = new TransferUtilityUploadRequest
        //                {
        //                    InputStream = ms,
        //                    Key = nombreArchivo,  //Path.GetFileName(rutaAchivo),
        //                    BucketName = string.Format("red-social/{0}", nombreDirectorio),
        //                    CannedACL = S3CannedACL.PublicRead
        //                };

        //                var fileTransferUtility = new TransferUtility(client);
        //                //await fileTransferUtility.UploadAsync(uploadRequest);
        //                fileTransferUtility.Upload(uploadRequest);

        //                LnPerfil lnPerfil = new LnPerfil();
        //                respuesta = lnPerfil.ModificarUrlImagenPorIdUsuario(Convert.ToInt64(entidad.IdUsuario), url);

        //            }
        //        }

        //    }
        //    catch (AmazonS3Exception exSe)
        //    {
        //        Log(Level.Error, String.Format("AmazonS3Exception: {0}", exSe));
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(Level.Error, String.Format("Exception: {0}", ex));
        //    }

        //    return respuesta;
        //}

        public int SubirImagenUsuarioAwsS3(AwsS3RegistrarUsuarioDto entidad, ref long nuevoId, ref string url)
        {
            int respuesta = 0;
            try
            {
                url = _urlAmazon;
                string nombreDirectorio = "Usuario";
                nuevoId = Convert.ToInt64(entidad.IdUsuario);

                AwsS3EliminarUsuarioDto prmEliminar = new AwsS3EliminarUsuarioDto();
                prmEliminar.IdUsuario = entidad.IdUsuario;
                int respuestaEliminar = EliminarImagenUsuarioAwsS3(prmEliminar);
                if (respuestaEliminar > 0)
                {
                    using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                    {
                        using (var ms = new MemoryStream(entidad.Archivo))
                        {
                            string nombreArchivo = string.Format("{0}_{1}{2}{3}_{4}{5}{6}.{7}",
                                entidad.IdUsuario,
                                DateTime.Now.Year.ToString("d4"),
                                DateTime.Now.Month.ToString("d2"),
                                DateTime.Now.Day.ToString("d2"),
                                DateTime.Now.Hour.ToString("d2"),
                                DateTime.Now.Minute.ToString("d2"),
                                DateTime.Now.Second.ToString("d2"),
                                entidad.ExtensionSinPunto);

                            url = string.Format("{0}{1}/{2}", url, nombreDirectorio, nombreArchivo);

                            var uploadRequest = new TransferUtilityUploadRequest
                            {
                                InputStream = ms,
                                Key = nombreArchivo,
                                BucketName = string.Format("red-social/{0}", nombreDirectorio),
                                CannedACL = S3CannedACL.PublicRead
                            };

                            var fileTransferUtility = new TransferUtility(client);
                            fileTransferUtility.Upload(uploadRequest);

                            LnUsuario lnUsuario = new LnUsuario();
                            respuesta = lnUsuario.ModificarUrlImagenPorIdUsuario(Convert.ToInt64(entidad.IdUsuario), url);

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

        public List<AwsS3ObtenerDto> ObtenerImagenUsuario(long idUsuario)
        {
            List<AwsS3ObtenerDto> lista = new List<AwsS3ObtenerDto>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls | SecurityProtocolType.Tls;
                string url = _urlAmazon;
                var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2);
                ListObjectsRequest request = new ListObjectsRequest();

                string prefijo = string.Format("Usuario/{0}_", idUsuario);
                //if(idPerfil > 0)
                //{
                //    prefijo = string.Format("Usuario/{0}_{1}_", idUsuario, idPerfil);
                //}

                request.BucketName = "red-social";
                request.Prefix = prefijo;
                request.Delimiter = "/";
                request.MaxKeys = 1000;

                //ListObjectsResponse response = client.ListObjectsAsync(request);
                var response = client.ListObjectsAsync(request);
                if(response != null)
                {
                    if (response.Result != null)
                    {
                        var x = response.Result.S3Objects;
                        if (x != null)
                        {
                            if (x.Any())
                            {
                                foreach (var objt in x)
                                {
                                    if (objt.Size > 0)
                                    {
                                        lista.Add(new AwsS3ObtenerDto
                                        {
                                            UrlImagen = string.Concat(url, objt.Key),
                                            FechaRegistro = objt.LastModified
                                        });
                                    }
                                }
                            }
                            
                        }
                    }
                }

                if(lista != null)
                {
                    if (lista.Any())
                    {
                        lista = lista.OrderByDescending(x => x.FechaRegistro).ToList();
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
            return lista;
        }

        public List<AwsS3ObtenerDto> ObtenerImagenAlbum(long idUsuario, long idAlbum)
        {
            List<AwsS3ObtenerDto> lista = new List<AwsS3ObtenerDto>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls | SecurityProtocolType.Tls;
                string url = _urlAmazon;
                var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2);
                ListObjectsRequest request = new ListObjectsRequest();

                string prefijo = string.Format("Album/{0}_", idUsuario);
                if(idAlbum > 0)
                {
                    string.Format("Album/{0}_{1}_", idUsuario, idAlbum);
                }

                request.BucketName = "red-social";
                request.Prefix = prefijo;
                request.Delimiter = "/";
                request.MaxKeys = 1000;

                //ListObjectsResponse response = client.ListObjectsAsync(request);
                var response = client.ListObjectsAsync(request);
                if (response != null)
                {
                    if (response.Result != null)
                    {
                        var x = response.Result.S3Objects;
                        if (x != null)
                        {
                            if (x.Any())
                            {
                                foreach (var objt in x)
                                {
                                    if (objt.Size > 0)
                                    {
                                        lista.Add(new AwsS3ObtenerDto
                                        {
                                            UrlImagen = string.Concat(url, objt.Key),
                                            FechaRegistro = objt.LastModified
                                        });
                                    }
                                }
                            }

                        }
                    }
                }

                if (lista != null)
                {
                    if (lista.Any())
                    {
                        lista = lista.OrderByDescending(x => x.FechaRegistro).ToList();
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
            return lista;
        }

        public List<AwsS3ObtenerDto> ObtenerImagenGaleria(long idUsuario, long idAlbum, long idImagen)
        {
            List<AwsS3ObtenerDto> lista = new List<AwsS3ObtenerDto>();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls | SecurityProtocolType.Tls;
                string url = _urlAmazon;
                var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2);
                ListObjectsRequest request = new ListObjectsRequest();

                string prefijo = string.Format("Imagen/{0}_{1}_", idUsuario, idAlbum);
                if(idImagen > 0)
                {
                    prefijo = string.Format("Imagen/{0}_{1}_{2}_", idUsuario, idAlbum, idImagen);
                }

                request.BucketName = "red-social";
                request.Prefix = prefijo;
                request.Delimiter = "/";
                request.MaxKeys = 1000;

                //ListObjectsResponse response = client.ListObjectsAsync(request);
                var response = client.ListObjectsAsync(request);
                if (response != null)
                {
                    if (response.Result != null)
                    {
                        var x = response.Result.S3Objects;
                        if (x != null)
                        {
                            if (x.Any())
                            {
                                foreach (var objt in x)
                                {
                                    if (objt.Size > 0)
                                    {
                                        lista.Add(new AwsS3ObtenerDto
                                        {
                                            UrlImagen = string.Concat(url, objt.Key),
                                            FechaRegistro = objt.LastModified
                                        });
                                    }
                                }
                            }

                        }
                    }
                }

                if (lista != null)
                {
                    if (lista.Any())
                    {
                        lista = lista.OrderByDescending(x => x.FechaRegistro).ToList();
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
            return lista;
        }

        public int EliminarAlbumAwsS3(AwsS3EliminarAlbumDto entidad)
        {
            int respuesta = 0;
            try
            {
                
                string nombreDirectorio = "Album";

                string url = string.Format("{0}{1}/", _urlAmazon, nombreDirectorio);
                string nombreArchivo = entidad.UrlImagen.Replace(url, string.Empty);

                using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                {
                    var deleteObjectRequest = new DeleteObjectRequest
                    {
                        Key = nombreArchivo,
                        BucketName = string.Format("red-social/{0}", nombreDirectorio)
                    };

                    Task eliminar = Task.Run(()=>
                    {
                        client.DeleteObjectAsync(deleteObjectRequest);
                    });

                    eliminar.Wait();

                    if (eliminar.IsCompleted)
                    {
                        //eliminar de la base de datos
                        LnAlbumImagen lnAlbumImagen = new LnAlbumImagen();
                        int resultadoEliminarBd = lnAlbumImagen.EliminarImagen(entidad.IdAlbumImagen);
                        if (resultadoEliminarBd > 0)
                        {
                            respuesta = 1;
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

        public int EliminarImagenGaleriaAwsS3(AwsS3EliminarGaleriaDto entidad)
        {
            int respuesta = 0;
            try
            {

                string nombreDirectorio = "Imagen";

                string url = string.Format("{0}{1}/", _urlAmazon, nombreDirectorio);
                string nombreArchivo = entidad.UrlImagen.Replace(url, string.Empty);

                using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                {
                    var deleteObjectRequest = new DeleteObjectRequest
                    {
                        Key = nombreArchivo,
                        BucketName = string.Format("red-social/{0}", nombreDirectorio)
                    };

                    Task eliminar = Task.Run(() =>
                    {
                        client.DeleteObjectAsync(deleteObjectRequest);
                    });

                    eliminar.Wait();

                    if (eliminar.IsCompleted)
                    {
                        //eliminar de la base de datos
                        LnImagen lnImagen = new LnImagen();
                        int resultadoEliminarBd = lnImagen.Eliminar(entidad.IdImagen);
                        if (resultadoEliminarBd > 0)
                        {
                            respuesta = 1;
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

        public int EliminarImagenUsuarioAwsS3(AwsS3EliminarUsuarioDto entidad)
        {
            int respuesta = 0;
            try
            {

                string nombreDirectorio = "Usuario";

                string url = string.Format("{0}{1}/", _urlAmazon, nombreDirectorio);
                LnUsuario lnUsuario = new LnUsuario();
                var usuarioImagen = lnUsuario.ObtenerUrlImagenPorId(entidad.IdUsuario);
                if (!string.IsNullOrEmpty(usuarioImagen.UrlImagen) && !usuarioImagen.UrlImagen.Equals("https://red-social.s3.us-east-2.amazonaws.com/Aplicativo/sin_foto_perfil.jpg"))
                {
                    string nombreArchivo = usuarioImagen.UrlImagen.Replace(url, string.Empty);
                    using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
                    {
                        var deleteObjectRequest = new DeleteObjectRequest
                        {
                            Key = nombreArchivo,
                            BucketName = string.Format("red-social/{0}", nombreDirectorio)
                        };

                        Task eliminar = Task.Run(() =>
                        {
                            client.DeleteObjectAsync(deleteObjectRequest);
                        });

                        eliminar.Wait();

                        if (eliminar.IsCompleted)
                        {
                            //eliminar de la base de datos
                            int resultadoEliminarBd = lnUsuario.EliminarUrlImagen(entidad.IdUsuario);
                            if (resultadoEliminarBd > 0)
                            {
                                respuesta = 1;
                            }
                        }
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
