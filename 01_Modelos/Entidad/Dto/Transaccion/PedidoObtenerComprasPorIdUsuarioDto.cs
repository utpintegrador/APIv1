using Newtonsoft.Json;

namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerComprasPorIdUsuarioDto
    {
        public long IdPedido { get; set; }
        public string DocumentoIdentificacionVendedor { get; set; }
        public string NombreNegocioVendedor { get; set; }
        public string DocumentoIdentificacionComprador{ get; set; }
        public string NombreNegocioComprador{ get; set; }
        public string Direccion { get; set; }
        public string DescripcionMoneda { get; set; }
        public string DescripcionEstado { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaActualizacion { get; set; }
        public decimal Total { get; set; }
        [JsonIgnore]
        public long TotalItems { get; set; }
    }
}
