using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class MonedaObtenerDto
    {
        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public string Simbolo { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
