using Entidad.Configuracion.Entidad;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Xml;

namespace Entidad.Configuracion.Proceso
{
    public class Logger
    {
        #region "Implementar Log4Net"

        private static ILog GetLogger()
        {
            return LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region "Obtener nombre archivo Log"

        private static string GetFileName()
        {
            var mes = DateTime.Now.Month.ToString("00");
            var anio = DateTime.Now.Year.ToString();
            return string.Format(@"{0}\{1}\{2}\", ConfiguracionJson.Conf.RutaLog, anio, mes);
        }

        #endregion

        #region "GetLog"

        private static Log GetLog(StackTrace stackTrace)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            string auxEspacio = new string(' ', 60);
            string proyecto = ConfiguracionJson.Conf.NombreLog; 
            var clase = string.Empty;
            var declaringType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            if (declaringType != null)
            {

                clase = declaringType.Name;
                clase = string.Concat(clase, auxEspacio).Substring(0, 30);
            }

            string auxMetodo = stackTrace.GetFrame(1).GetMethod().Name;

            return new Log
            {
                Usuario = Environment.UserName,
                Ip = GetLocalIpAddress(),
                Proyecto = proyecto,
                Clase = clase,
                Metodo = string.Concat(auxMetodo, auxEspacio).Substring(0, 42),
                NombreArchivo = string.Concat(proyecto, @"_", DateTime.Now.ToString("yyyyMMdd").Trim())
            };
        }

        #endregion

        #region "Obtener plantilla"

        private static PatternLayout GetLayout()
        {
            var layout = new PatternLayout
            {
                //ConversionPattern =
                //    @"%date{hh:mm:ss} [%thread] %-5level  [%property{Ip}] [%property{Usuario}]   [%property{Proyecto}]    [%property{Clase}]    [%property{Metodo}]    - %message%newline"
                ConversionPattern =
                    @"%date{HH:mm:ss:fff} %-5level  [%property{Ip}] [%property{Clase}]    [%property{Metodo}]    - %message%newline"
            };
            layout.ActivateOptions();
            return layout;
        }

        #endregion

        #region "Crear archivo Log"

        private static FileAppender GetAppender(Log log)
        {
            var appenderFile = new FileAppender
            {
                Name = log.NombreArchivo,
                File = string.Concat(GetFileName(), log.NombreArchivo, @".log"),
                AppendToFile = true,
                Layout = GetLayout()
            };
            appenderFile.ActivateOptions();
            return appenderFile;
        }

        private static void GetProperties(Log log)
        {
            ThreadContext.Properties["Ip"] = log.Ip;
            ThreadContext.Properties["Proyecto"] = log.Proyecto;
            ThreadContext.Properties["Clase"] = log.Clase;
            ThreadContext.Properties["Metodo"] = log.Metodo;
            ThreadContext.Properties["Usuario"] = log.Usuario;
        }

        #endregion

        #region "Log"

        public static void Log(Level logLevel, string msg)
        {
            var rutaConfig = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"log4Net.xml"));
            if (!File.Exists(rutaConfig))
            {
                rutaConfig = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log4Net.xml"));
            }

            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
               typeof(log4net.Repository.Hierarchy.Hierarchy));

            if (!File.Exists(rutaConfig))
            {
                XmlConfigurator.Configure(repo);
            }
            else
            {
                XmlDocument log4netConfig = new XmlDocument();
                log4netConfig.Load(File.OpenRead(rutaConfig));
            }

            XmlConfigurator.Configure(repo, new FileInfo(rutaConfig));

            var logger = GetLogger();
            var stackTrace = new StackTrace();
            var log = GetLog(stackTrace);
            var appenderFile = GetAppender(log);
            ((log4net.Repository.Hierarchy.Logger)logger.Logger).AddAppender(appenderFile);

            GetProperties(log);

            switch (logLevel)
            {
                case Level.Debug: logger.Debug(msg); break;
                case Level.Error: logger.Error(msg); break;
                case Level.Fatal: logger.Fatal(msg); break;
                case Level.Info: logger.Info(msg); break;
                case Level.Warn: logger.Warn(msg); break;
            }
        }

        public enum Level
        {
            Debug,
            Info,
            Warn,
            Error,
            Fatal,
        }

        #endregion

        #region "Obtener ip Local"

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");

        }

        #endregion
    }
}
