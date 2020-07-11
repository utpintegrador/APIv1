namespace Entidad.Dto.Maestro
{
    public class ProductoAtributoDescuentoDto
    {
        public long? IdProductoDescuento { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public decimal? Valor { get; set; }
        public string DescripcionTipoDescuento { get; set; }
        public string DescripcionEstadoDescuento { get; set; }
    }
}
