using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class TipoEstadoObtenerDto
    {
        public int IdTipoEstado { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
