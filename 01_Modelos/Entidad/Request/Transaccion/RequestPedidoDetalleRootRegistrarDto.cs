using System.Collections.Generic;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoDetalleRootRegistrarDto
    {
        public List<RequestPedidoDetalleRegistrarDto> ListaPedidoDetalle { get; set; }
        public RequestPedidoDetalleRootRegistrarDto()
        {
            ListaPedidoDetalle = new List<RequestPedidoDetalleRegistrarDto>();
        }
    }
}
