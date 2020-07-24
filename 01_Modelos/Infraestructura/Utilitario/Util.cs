using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructura.Utilitario
{
    public class Util
    {
        private static string IV = "HS2257G%&V1kde2y";
        private static string Key = "jrewg212IUKJjndht25ertg254dfgtrh";
        public static bool EsEntero(object dato)
        {
            try
            {
                int convert = Convert.ToInt32(dato);
                return true;
            }
            catch
            {

            }
            return false;
        }

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

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            //for (int i = 0; i < properties.Count -1; i++)
            //{
            //    PropertyDescriptor prop = properties[i];
            //    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            //}

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;


            /*
             Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
            Dim table As New DataTable()
            For i As Integer = 0 To props.Count - 1
                Dim prop As PropertyDescriptor = props(i)
                'table.Columns.Add(prop.Name, prop.PropertyType);

                table.Columns.Add(prop.Name, If(Nullable.GetUnderlyingType(prop.PropertyType), prop.PropertyType))
            Next
            Dim values As Object() = New Object(props.Count - 1) {}
            For Each item As T In data
                For i As Integer = 0 To values.Length - 1
                    values(i) = props(i).GetValue(item)
                Next
                table.Rows.Add(values)
            Next
            Return table
             */


        }

        public static double Haversine(double lat1, double lon1,
                        double lat2, double lon2)
        {
            // distance between latitudes and longitudes 
            double dLat = (Math.PI / 180) * (lat2 - lat1);
            double dLon = (Math.PI / 180) * (lon2 - lon1);

            // convert to radians 
            lat1 = (Math.PI / 180) * (lat1);
            lat2 = (Math.PI / 180) * (lat2);

            // apply formulae 
            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Pow(Math.Sin(dLon / 2), 2) *
                       Math.Cos(lat1) * Math.Cos(lat2);
            double rad = 6371;
            double c = 2 * Math.Asin(Math.Sqrt(a));
            return rad * c;
        }
    }
}
