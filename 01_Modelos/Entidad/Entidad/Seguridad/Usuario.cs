using System;

namespace Entidad.Entidad.Seguridad
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        //public string UserName { get; set; }
        //public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoUsuario { get; set; }
        public string UrlImagen { get; set; }        

    }
}
