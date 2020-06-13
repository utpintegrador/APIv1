namespace Entidad.Configuracion.Entidad
{
    public class Config
    {
        public string Servidor { get; set; }
        public string BaseDatos { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string RutaLog { get; set; }
        public string RutaArchivos { get; set; }
        public string NombreLog { get; set; }
        public string Cn { get; set; }

        public Config()
        {
            Cn = string.Empty;
            NombreLog = "FcTest";
        }

    }
}
