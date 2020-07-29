using System.Collections.Generic;

namespace Entidad.Dto.Transaccion
{
    public class PedidoAtributoDto
    {
        public long IdPedido { get; set; }
        public long IdNegocioVendedor { get; set; }
        public long IdNegocioComprador { get; set; }
        public string Direccion { get; set; }
        public int IdMoneda { get; set; }
        public int IdEstado { get; set; }
        public decimal Total { get; set; }
        public string NumeroCelular { get; set; }
        public string Observaciones { get; set; }
        public List<PedidoAtributoDetalleDto> ListaDetalle { get; set; }
        public PedidoAtributoDto()
        {
            ListaDetalle = new List<PedidoAtributoDetalleDto>();
        }
    }
}
