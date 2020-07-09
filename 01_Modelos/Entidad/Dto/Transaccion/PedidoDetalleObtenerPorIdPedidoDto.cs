namespace Entidad.Dto.Transaccion
{
    public class PedidoDetalleObtenerPorIdPedidoDto
    {
        public long IdPedidoDetalle { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
