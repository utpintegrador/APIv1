using Datos.Repositorio.Correo;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Correo;
using NVelocityTemplateEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Negocio.Repositorio.Servicio
{
    public class LnRecuperacionContrasenia: Logger
    {
        private readonly AdRecuperacionContrasenia _adRecuperacionContrasenia = new AdRecuperacionContrasenia();
                
        public Boolean ProcesarAlertaRecuperacionContrasenia()
        {
            try
            {
                var listado = ObtenerPendientesProceso();
                if(listado != null)
                {
                    if (listado.Any())
                    {
                        foreach (var item in listado)
                        {
                            ProcesarAlertaRecuperacionIndividual(item);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return false;
        }

        private void ProcesarAlertaRecuperacionIndividual(RecuperacionContraseniaObtenerDto entidad)
        {
            try
            {
                string cuerpoHtml = ProcesarPlantillaVm(entidad, "RecuperacionContrasenia.vm");
                if (string.IsNullOrEmpty(cuerpoHtml)) return;
                if (cuerpoHtml.ToLower().Contains("encountered") ||
                    cuerpoHtml.ToLower().Contains("lexical error"))
                {
                    //Log(Level.Error, String.Format("IdTransaccion: {0} ; error al procesar la plantilla: {1}",
                    //    alerta.IdTransaccion,
                    //    cuerpoHtml));
                }
                else
                {
                    List<string> destinatarios = new List<string>();
                    destinatarios.Add(entidad.CorreoElectronico);
                    if (EnviarMensaje(destinatarios,
                        "Solicitud de recuperacion de contraseña",
                        cuerpoHtml))
                    {
                        Log(Level.Info, string.Format("Se envió alerta informando datos erroneos en la trama recibida"));

                        int resultadoMarcarCorreoEnviado = ModificarEstadoEnviado(entidad.IdRecuperacionContrasenia);
                        if (resultadoMarcarCorreoEnviado > 0)
                        {
                            Log(Level.Info, String.Format("Se actualizó el indicador de correo informando que se ha enviado satisfactoriamente"));
                        }

                    }
                    else
                    {
                        Log(Level.Warn, String.Format("No pudo enviarse alerta informando datos erroneos en la trama recibida"));
                    }

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        private string ProcesarPlantillaVm(RecuperacionContraseniaObtenerDto cuerpoMensaje, string nombrePlantilla)
        {
            try
            {
                var directorioPlantilla = Path.Combine(ConfiguracionJson.Conf.RutaArchivos, "Plantillas");
                var fileEngine = NVelocityEngineFactory.CreateNVelocityFileEngine(directorioPlantilla, true);
                IDictionary context = new Hashtable();
                context.Add("datos", cuerpoMensaje);
                string html = fileEngine.Process(context, nombrePlantilla);
                return html;
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return string.Empty;
            }

        }

        private static bool EnviarMensaje(List<string> listaPara,
            string asunto,
            string cuerpoHtml,
            MemoryStream ms = null,
            string nomArchivo = "")
        {
            try
            {
                string servidorCorreo = "mail.control-zeta.net";
                string desde = "fcochachin@control-zeta.net";
                using (SmtpClient objCorreo = new SmtpClient(servidorCorreo))
                {
                    objCorreo.Port = 25;
                    using (MailMessage objMensaje = new MailMessage())
                    {
                        NetworkCredential NC = new NetworkCredential();
                        NC.UserName = "fcochachin@control-zeta.net";
                        NC.Password = "fcochachin@123";
                        objCorreo.Credentials = NC;
                        //MailAddress from = new MailAddress(desde, "After-Class");

                        objMensaje.From = new MailAddress(desde, "EncuentraloYa");
                        objMensaje.IsBodyHtml = true;
                        objMensaje.Subject = asunto;
                        objMensaje.Body = cuerpoHtml;
                        objMensaje.BodyEncoding = Encoding.UTF8;
                        
                        foreach (String strTo in listaPara)
                        {
                            objMensaje.To.Add(strTo);
                        }

                        if (ms != null)
                        {

                            using (ms)
                            {
                                objMensaje.Attachments.Add(new Attachment(ms, nomArchivo));
                                objCorreo.Send(objMensaje);
                            }
                        }
                        else
                        {
                            objCorreo.Send(objMensaje);
                        }

                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
                return false;
            }

        }

        public RecuperacionContraseniaObtenerRegistradoDto Registrar(string correoElectronico)
        {
            return _adRecuperacionContrasenia.Registrar(correoElectronico);
        }

        private List<RecuperacionContraseniaObtenerDto> ObtenerPendientesProceso()
        {
            return _adRecuperacionContrasenia.ObtenerPendientesProceso();
        }

        private int ModificarEstadoEnviado(long idRecuperacionContrasenia)
        {
            return _adRecuperacionContrasenia.ModificarEstadoEnviado(idRecuperacionContrasenia);
        }

        public int EliminarProcesados()
        {
            return _adRecuperacionContrasenia.EliminarProcesados();
        }

        public RecuperacionContraseniaObtenerPorCodigoDto ObtenerUsuarioPorCodigo(string codigo)
        {
            return _adRecuperacionContrasenia.ObtenerUsuarioPorCodigo(codigo);
        }

        public int ModificarContraseniaMedianteCodigo(RecuperacionContraseniaModificarContraseniaDto modelo)
        {
            return _adRecuperacionContrasenia.ModificarContraseniaMedianteCodigo(modelo);
        }

    }
}
