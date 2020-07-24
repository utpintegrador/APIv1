namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerPendientesAtencionPorIdUsuarioDto
    {
        public long IdPedido { get; set; }
        public string FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public decimal Total { get; set; }

    }
}
