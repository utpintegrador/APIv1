using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class TipoDocumentoIdentificacionObtenerDto
    {
        public int IdTipoDocumentoIdentificacion { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
