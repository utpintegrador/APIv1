using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class ProductoDescuentoObtenerPorIdProductoDto
    {
        public long IdProductoDescuento { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string DescripcionTipoDescuento { get; set; }
        public decimal Valor { get; set; }
        public string DescripcionEstado { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
