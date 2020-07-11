using System;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioLoginDto
    {
        public string Token { get; set; }
        public DateTime? Expiracion { get; set; }
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string UrlImagen { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseUsuarioLoginDto()
        {
            ListaError = new List<ErrorDto>();
        }
    }
}
