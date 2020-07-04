using System;

namespace Entidad.Vo
{
    public static class ConstanteVo
    {
        public static string AccessKeyAws = "";
        public static string SecretAccessKeyAws = "";
        public static string UrlAmazon = "";

        //public static string NombreDominio = "https://localhost:44314";
        public static string NombreDominio = "Indice de métodos: " +
            Environment.NewLine +
            " * http://api-find.control-zeta.net/swagger" +
            //Environment.NewLine +
            //" * http://www.control-zeta.somee.com/swagger (se deshabilitara)" +

            Environment.NewLine +
            Environment.NewLine +
            "Ejemplo de invocacion: " +
            Environment.NewLine +
            " * http://api-find.control-zeta.net/api/Usuario" +
            //Environment.NewLine +
            //" * http://www.control-zeta.somee.com (se deshabilitara)" +

            //Environment.NewLine +
            //Environment.NewLine +
            //"Ejemplo de invocación:" +
            //Environment.NewLine +
            //" * http://www.control-zeta.somee.com/api/Carrera (este dominio se deshabilitara)" +
            //Environment.NewLine +
            //" * http://www.control-zeta.somee.com/api/Carrera/1 (este dominio se deshabilitara)" +
            //Environment.NewLine +
            //Environment.NewLine +
            //"Convertir archivo en Base64" +
            //Environment.NewLine +
            //" * https://base64.guru/converter/encode/file" +
            Environment.NewLine +
            Environment.NewLine +
            "Los parametros fecha deberan ser enviados en string con el formato:" +
            Environment.NewLine +
            " * yyyy/MM/dd HH:MM:SS (Fecha y Hora)" +
            Environment.NewLine +
            " * yyyy/MM/dd (Solo Fecha)" +
            //Environment.NewLine +
            //Environment.NewLine +
            //"La base de datos de SOMEE queda deshabilitada, solo se usara la de control-zeta.net" +
            Environment.NewLine + 
            Environment.NewLine +
            "Actualizado al 04/07/2020" +
            Environment.NewLine +
            Environment.NewLine +
            "Los modelos en Android se deben generar desde esta pagina" +
            Environment.NewLine +
            "http://www.jsonschema2pojo.org/";

    }
}
