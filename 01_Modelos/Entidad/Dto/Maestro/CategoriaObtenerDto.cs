using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class CategoriaObtenerDto
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public string DescripcionEstado { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaActualizacion { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
