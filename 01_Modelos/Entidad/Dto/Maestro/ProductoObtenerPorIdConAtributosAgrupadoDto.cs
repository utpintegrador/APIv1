namespace Entidad.Dto.Maestro
{
    public class ProductoObtenerPorIdConAtributosAgrupadoDto
    {
        public long IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionExtendida { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdCategoria { get; set; }
        public long IdNegocio { get; set; }
        public int IdEstado { get; set; }
        public long? IdProductoImagen { get; set; }
        public string UrlImagen { get; set; }
        public bool? Predeterminado { get; set; }
        public long? IdProductoDescuento { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public decimal? Valor { get; set; }
        public string DescripcionTipoDescuento { get; set; }
        public string DescripcionEstadoDescuento { get; set; }

    }
}
