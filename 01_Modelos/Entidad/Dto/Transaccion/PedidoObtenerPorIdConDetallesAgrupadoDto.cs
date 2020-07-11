namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerPorIdConDetallesAgrupadoDto
    {
        public long IdPedido { get; set; }
        public long IdNegocioVendedor { get; set; }
        public long IdNegocioComprador { get; set; }
        public string Direccion { get; set; }
        public int IdMoneda { get; set; }
        public int IdEstado { get; set; }


        public long? IdPedidoDetalle { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public string DescripcionProducto { get; set; }

    }
}
