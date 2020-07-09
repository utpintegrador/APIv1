using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class TipoDescuentoObtenerDto
    {
        public int IdTipoDescuento { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
