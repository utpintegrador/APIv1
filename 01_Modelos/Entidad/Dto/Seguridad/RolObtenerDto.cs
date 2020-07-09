using Newtonsoft.Json;

namespace Entidad.Dto.Seguridad
{
    public class RolObtenerDto
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
