namespace Entidad.Dto.Maestro
{
    public class ProductoDescuentoObtenerPorIdDto
    {
        public long IdProductoDescuento { get; set; }
        public long IdProducto { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int IdTipoDescuento { get; set; }
        public decimal Valor { get; set; }
        public int IdEstado { get; set; }

    }
}
