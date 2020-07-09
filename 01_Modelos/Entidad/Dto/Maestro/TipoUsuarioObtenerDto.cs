using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class TipoUsuarioObtenerDto
    {
        public int IdTipoUsuario { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
