using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class TipoBusquedaObtenerDto
    {
        public int IdTipoBusqueda { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
