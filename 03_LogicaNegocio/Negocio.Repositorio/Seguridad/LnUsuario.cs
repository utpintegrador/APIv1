﻿using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Datos.Repositorio.Seguridad;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using Entidad.Vo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Negocio.Repositorio.Seguridad
{
    public class LnUsuario: Logger
    {
        private readonly AdUsuario _adUsuario = new AdUsuario();
        public UsuarioLoginDto ObtenerPorLogin(RequestUsuarioCredencialesDto modelo)
        {
            modelo.Contrasenia = Infraestructura.Utilitario.Util.Encriptar(modelo.Contrasenia.Trim());
            var usuarioLogin = _adUsuario.ObtenerPorLogin(modelo);
            if(usuarioLogin != null)
            {
                
                LnRol lnRol = new LnRol();
                var listadoRol = lnRol.ObtenerPorIdUsuario(usuarioLogin.IdUsuario);
                if(listadoRol != null)
                {
                    if (listadoRol.Any())
                    {
                        usuarioLogin.ListaRol = new List<RolObtenerPorIdUsuarioDto>();
                        usuarioLogin.ListaRol = listadoRol;
                    }
                }
            }
            return usuarioLogin;
        }

        public List<UsuarioObtenerDto> Obtener(RequestUsuarioObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestUsuarioObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdUsuario";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            return _adUsuario.Obtener(filtro);
        }

        public UsuarioObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adUsuario.ObtenerPorId(id);
        }

        public int Registrar(RequestUsuarioRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            modelo.Contrasenia = Infraestructura.Utilitario.Util.Encriptar(modelo.Contrasenia.Trim());
            //Al guardar usuario tambien se guarda en la tabla RolUsuario por lo cual se emplea transaccion
            using (var scope = new TransactionScope())
            {
                resultado = _adUsuario.Registrar(modelo, ref idNuevo);
                if (idNuevo > 0)
                {
                    scope.Complete();
                }
                else
                {
                    resultado = 0;
                }
            }
            return resultado;
        }

        public int Modificar(RequestUsuarioModificarDto modelo)
        {
            return _adUsuario.Modificar(modelo);
        }

        public int ModificarModoAdmin(RequestUsuarioModificarModoAdminDto modelo)
        {
            return _adUsuario.ModificarModoAdmin(modelo);
        }

        public int Eliminar(long id)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                respuesta = _adUsuario.Eliminar(id);
                if (respuesta > 0)
                {
                    scope.Complete();
                }
            }
            return respuesta;
        }

        public int ModificarContrasenia(RequestUsuarioCambioContraseniaDto modelo)
        {
            modelo.Contrasenia = Infraestructura.Utilitario.Util.Encriptar(modelo.Contrasenia);
            return _adUsuario.ModificarContrasenia(modelo);
        }

        public int ModificarUrlImagenPorIdUsuario(long idUsuario, string url)
        {
            return _adUsuario.ModificarUrlImagenPorIdUsuario(idUsuario, url);
        }

        //public UsuarioObtenerUrlImagenDto ObtenerUrlImagenPorId(long id)
        //{
        //    return _adUsuario.ObtenerUrlImagenPorId(id);
        //}

        //public int EliminarUrlImagen(long id)
        //{
        //    return _adUsuario.EliminarUrlImagen(id);
        //}

        public async Task<string> SubirImagenAws(RequestUsuarioModificarImagenMetodo1Dto entidad)
        {
            //int respuesta = 0;
            string url = string.Empty;
            try
            {
                var objetoImagenBd = _adUsuario.ObtenerUrlImagenPorId(entidad.IdUsuario);
                if (objetoImagenBd == null)
                {
                    //url = string.Empty;
                    //return -1;
                    url = "-1";
                    return url;
                }

                url = ConstanteVo.UrlAmazon;
                string nombreDirectorio = "Usuario";

                EliminarImagenAws(objetoImagenBd.UrlImagen, entidad.IdUsuario);
                //int respuestaEliminar = 
                //if (respuestaEliminar > 0)
                //{
                using (var client = new AmazonS3Client(
                    Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.AccessKeyAws),
                    Infraestructura.Utilitario.Util.Desencriptar(ConstanteVo.SecretAccessKeyAws),
                    RegionEndpoint.USEast2))
                {
                    string nombreArchivo = string.Format("{0}_{1}{2}{3}_{4}{5}{6}_{7}.{8}",
                            entidad.IdUsuario,
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
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }
                }

                //LnUsuario lnUsuario = new LnUsuario();
                int respuestaBd = ModificarUrlImagenPorIdUsuario(entidad.IdUsuario, url);
                if (respuestaBd == 0)
                {
                    url = "0";
                }

                //}
            }
            catch (AmazonS3Exception exSe)
            {
                Log(Level.Error, String.Format("AmazonS3Exception: {0}", exSe));
            }
            catch (Exception ex)
            {
                Log(Level.Error, String.Format("Exception: {0}", ex));
            }

            return url;// respuesta;
        }

        public int EliminarImagen(long idUsuario, ref string urlImagen)
        {
            int respuesta = 0;
            try
            {
                var objetoImagenBd = _adUsuario.ObtenerUrlImagenPorId(idUsuario);
                if (objetoImagenBd == null)
                {
                    urlImagen = string.Empty;
                    respuesta = -1;
                }
                else
                {
                    respuesta = EliminarImagenAws(objetoImagenBd.UrlImagen, idUsuario);
                    int respuestaModificarImagenBd = _adUsuario.EliminarUrlImagen(idUsuario);
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

        private int EliminarImagenAws(string urlImagenBd, long idUsuario)
        {
            int respuesta = 0;
            try
            {
                if(!string.IsNullOrEmpty(urlImagenBd))
                {
                    if (urlImagenBd != "https://encuentralo.s3.us-east-2.amazonaws.com/Aplicativo/sin_foto_perfil.jpg")
                    {
                        string nombreDirectorio = "Usuario";

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

        public List<UsuarioObtenerComboDto> ObtenerCombo(int idEstado)
        {
            var listado = _adUsuario.ObtenerCombo(idEstado);
            if (listado == null)
            {
                listado = new List<UsuarioObtenerComboDto>();
            }
            return listado;
        }

        public UsuarioObtenerContraseniaPorIdDto ObtenerContraseniaPorId(long id)
        {
            return _adUsuario.ObtenerContraseniaPorId(id);
        }
    }
}
