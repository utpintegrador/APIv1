using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class ProductoImagenObtenerPorIdProductoDto
    {
        public long IdProductoImagen { get; set; }
        public string UrlImagen { get; set; }
        public bool Predeterminado { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
