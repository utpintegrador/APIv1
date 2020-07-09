using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class EstadoObtenerDto
    {
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionTipoEstado { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
