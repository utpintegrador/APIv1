namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerPorIdDto
    {
        public long IdPedido { get; set; }
        public long IdNegocioVendedor { get; set; }
        public long IdNegocioComprador { get; set; }
        public string Direccion { get; set; }
        public int IdMoneda { get; set; }
        public int IdEstado { get; set; }
    }
}
