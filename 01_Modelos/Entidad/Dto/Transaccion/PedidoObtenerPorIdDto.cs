using Newtonsoft.Json;

namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerPorIdDto
    {
        public long IdPedido { get; set; }
        public long IdNegocioVendedor { get; set; }
        public long IdNegocioComprador { get; set; }
        public string Direccion { get; set; }
        public string NumeroCelular { get; set; }
        public string Observaciones { get; set; }
        public int IdMoneda { get; set; }
        public int IdEstado { get; set; }
        [JsonIgnore]
        public decimal Total { get; set; }
    }
}
