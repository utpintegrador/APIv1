namespace Entidad.Dto.Seguimiento
{
    public class PedidoControlEstadoObtenerPorIdPedidoDto
    {
        public long IdPedidoControlEstado { get; set; }
        public string DescripcionEstado { get; set; }
        public string FechaRegistro { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
