using System;

namespace Entidad.Dto.Seguridad
{
    public class UsuarioObtenerDto
    {
        public int IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        //public string UserName { get; set; }
        //cuidado en que metodos se retorna
        //public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DescripcionEstado { get; set; }
        public string DescripcionTipoUsuario { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaActualizacion { get; set; }
        //public string UrlImagen { get; set; }
        public int TotalItems { get; set; }
    }
}
