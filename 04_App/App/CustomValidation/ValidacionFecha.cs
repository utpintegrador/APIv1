using System;

namespace App.CustomValidation
{
    public class ValidacionFecha
    {
        public static bool ValidarFechaNula(string dato, ref DateTime? fecha)
        {
            try
            {
                fecha = null;
                //Si se aceptan fechas nulas
                if (string.IsNullOrEmpty(dato))
                {
                    return true;
                }

                //yyyy/MM/dd HH:mm:ss (Fecha y Hora 24)
                if (dato.Trim().Length == 10 || dato.Trim().Length == 19)
                {
                    if (dato.Trim().Length == 10)
                    {
                        fecha = new DateTime(
                            Convert.ToInt32(dato.Substring(0, 4)),
                            Convert.ToInt32(dato.Substring(5, 2)),
                            Convert.ToInt32(dato.Substring(8, 2))
                                            );
                        return true;
                    }

                    if (dato.Trim().Length == 19)
                    {
                        fecha = new DateTime(
                            Convert.ToInt32(dato.Substring(0, 4)),
                            Convert.ToInt32(dato.Substring(5, 2)),
                            Convert.ToInt32(dato.Substring(8, 2)),

                            Convert.ToInt32(dato.Substring(11, 2)),
                            Convert.ToInt32(dato.Substring(14, 2)),
                            Convert.ToInt32(dato.Substring(17, 2))
                                            );
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
