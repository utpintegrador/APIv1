using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Datos.Repositorio.Maestro;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System;
using System.Collections.Generic;
using System.IO;

namespace Negocio.Repositorio.Maestro
{
    public class LnCategoria: Logger
    {
        private readonly string _llaveAmazon = "AKIA2F7OY6EWJJ46KFVY";
        private readonly string _claveAmazon = "EncdXICIiJOcFgwB1u2ISQt5s0mOr8nTZAO0RiMu";
        private readonly string _urlAmazon = "https://encuentralo.s3.us-east-2.amazonaws.com/";

        private readonly AdCategoria _adCategoria = new AdCategoria();

        public List<CategoriaObtenerDto> Obtener()
        {
            return _adCategoria.Obtener();
        }

        public Categoria ObtenerPorId(int id)
        {
            return _adCategoria.ObtenerPorId(id);
        }

        public int Registrar(CategoriaRegistrarDto entidad, ref int nuevoId, ref string url)
        {
            //return _adCategoria.Registrar(modelo, ref idNuevo);
            int respuesta = 0;
            //try
            //{
            //    url = _urlAmazon;
            //    string nombreDirectorio = "Categoria";
            //    nuevoId = Convert.ToInt64(entidad.IdUsuario);

            //    AwsS3EliminarUsuarioDto prmEliminar = new AwsS3EliminarUsuarioDto();
            //    prmEliminar.IdUsuario = entidad.IdUsuario;
            //    int respuestaEliminar = EliminarImagenUsuarioAwsS3(prmEliminar);
            //    if (respuestaEliminar > 0)
            //    {
            //        using (var client = new AmazonS3Client(_llaveAmazon, _claveAmazon, RegionEndpoint.USEast2))
            //        {
            //            using (var ms = new MemoryStream(entidad.Archivo))
            //            {
            //                string nombreArchivo = string.Format("{0}_{1}{2}{3}_{4}{5}{6}.{7}",
            //                    entidad.IdUsuario,
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
            //                    Key = nombreArchivo,
            //                    BucketName = string.Format("red-social/{0}", nombreDirectorio),
            //                    CannedACL = S3CannedACL.PublicRead
            //                };

            //                var fileTransferUtility = new TransferUtility(client);
            //                fileTransferUtility.Upload(uploadRequest);

            //                LnUsuario lnUsuario = new LnUsuario();
            //                respuesta = lnUsuario.ModificarUrlImagenPorIdUsuario(Convert.ToInt64(entidad.IdUsuario), url);

            //            }
            //        }
            //    }
            //}
            //catch (AmazonS3Exception exSe)
            //{
            //    Log(Level.Error, String.Format("AmazonS3Exception: {0}", exSe));
            //}
            //catch (Exception ex)
            //{
            //    Log(Level.Error, String.Format("Exception: {0}", ex));
            //}

            return respuesta;
        }

        public int Modificar(Categoria modelo)
        {
            return _adCategoria.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adCategoria.Eliminar(id);
        }
    }
}
