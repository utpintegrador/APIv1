using Entidad.Configuracion.Entidad;
using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Entidad.Configuracion.Proceso
{
    public class ConfiguracionJson: Logger
    {
        public static Config Conf = new Config();

        public static void EstablecerConfiguracion()
        {
            try
            {

                const string nombreArchivo = "Configuracion.json";
                var rutaArchivos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archivos");

                var rutaJson = Path.Combine(rutaArchivos, nombreArchivo);
                if (File.Exists(rutaJson))
                {
                    Conf = JsonConvert.DeserializeObject<Config>(File.ReadAllText(rutaJson));
                    Conf.RutaArchivos = rutaArchivos;
                }

                if (string.IsNullOrEmpty(Conf.RutaLog))
                {
                    Conf.RutaLog = Path.Combine(Conf.RutaArchivos, "Log");
                }

                Conf.Cn = ObtenerConeccionSql();

                //Log(Level.Info,
                //    string.Format(@"Se cargaron los parametros del archivo de configuración {0} correctamente.",
                //        nombreArchivo));

            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);

            }
        }

        private static string ObtenerConeccionSql()
        {

            var cn = $"Data Source={Conf.Servidor};Initial Catalog={Conf.BaseDatos};Integrated Security=false;User ID={Conf.Usuario};Password={Conf.Contrasenia};";

            return cn;
        }
    }
}
