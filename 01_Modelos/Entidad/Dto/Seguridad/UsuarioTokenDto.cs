using System;

namespace Entidad.Dto.Seguridad
{
    public class UsuarioTokenDto
    {
        public string Token { get; set; }
        public DateTime? Expiracion { get; set; }
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string UrlImagen { get; set; }
        //public string UserName { get; set; }
        //public string MensajeError { get; set; }
        //public int HttpStatusCode { get; set; }

        //public UsuarioTokenDto()
        //{
        //    HttpStatusCode = 200;
        //}
    }
}
