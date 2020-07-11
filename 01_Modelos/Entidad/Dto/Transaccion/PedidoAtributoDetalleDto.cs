namespace Entidad.Dto.Transaccion
{
    public class PedidoAtributoDetalleDto
    {
        public long? IdPedidoDetalle { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string DescripcionProducto { get; set; }
    }
}
