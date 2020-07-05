using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructura.Utilitario
{
    public class Util
    {
        private static string IV = "HS2257G%&V1kde2y";
        private static string Key = "jrewg212IUKJjndht25ertg254dfgtrh";

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

        public static string Encriptar(string textoDesencriptado)
        {
            byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(textoDesencriptado);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textBytes, 0, textBytes.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);
        }

        public static string Desencriptar(string textoEncriptado)
        {
            byte[] textBytes = Convert.FromBase64String(textoEncriptado);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textBytes, 0, textBytes.Length);
            icrypt.Dispose();

            //return Encoding.Unicode.GetString(enc);
            return ASCIIEncoding.ASCII.GetString(enc);
        }
    }
}
