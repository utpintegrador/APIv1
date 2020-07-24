namespace Entidad.Dto.Transaccion
{
    public class PedidoDetalleObtenerPorIdPedidoDto
    {
        public long IdPedidoDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string DescripcionProducto { get; set; }
        public string UrlImagenProducto { get; set; }
    }
}
