using System;
using System.Linq;

namespace Entidad.Utilitario
{
    public class Util
    {
        public static DateTime? ObtenerFechaDesdeString(string dato)
        {
            DateTime? fecha = null;
            try
            {

                if (string.IsNullOrEmpty(dato))
                {
                    return null;
                }
                dato = dato.Trim();

                if (dato.Length < 8)
                {
                    return null;
                }


                if (dato.Length == 8)
                {
                    //Solo fecha
                    int anio = Convert.ToInt32(dato.Substring(0, 4));
                    int mes = Convert.ToInt32(dato.Substring(4, 2));
                    int dia = Convert.ToInt32(dato.Substring(6, 2));
                    fecha = new DateTime(anio, mes, dia);
                }
                else if (dato.IndexOf(' ') > 0)
                {
                    var separado = dato.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (separado.Count >= 2)
                    {
                        //procesar los dos objetos
                        //Fecha
                        int anio = Convert.ToInt32(separado[0].Trim().Substring(0, 4));
                        int mes = Convert.ToInt32(separado[0].Trim().Substring(4, 2));
                        int dia = Convert.ToInt32(separado[0].Trim().Substring(6, 2));

                        //Hora
                        int hora = 0;
                        int minuto = 0;
                        int segundo = 0;
                        try
                        {
                            hora = Convert.ToInt32(separado[1].Trim().Substring(0, 2));
                            minuto = Convert.ToInt32(separado[1].Trim().Substring(3, 2));
                            segundo = Convert.ToInt32(separado[1].Trim().Substring(6, 2));
                        }
                        catch
                        {

                        }

                        fecha = new DateTime(anio, mes, dia, hora, minuto, segundo);
                    }
                }
            }
            catch
            {
                return null;
            }
            return fecha;
        }
    }
}
