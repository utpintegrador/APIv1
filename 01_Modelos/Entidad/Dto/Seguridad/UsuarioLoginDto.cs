using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entidad.Dto.Seguridad
{
    public class UsuarioLoginDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string UrlImagen { get; set; }
        [JsonIgnore]
        public List<RolObtenerPorIdUsuarioDto> ListaRol { get; set; }
        public UsuarioLoginDto()
        {
            ListaRol = new List<RolObtenerPorIdUsuarioDto>();
        }
    }
}
