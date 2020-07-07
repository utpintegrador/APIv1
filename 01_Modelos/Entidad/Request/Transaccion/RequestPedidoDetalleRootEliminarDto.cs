using System.Collections.Generic;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoDetalleRootEliminarDto
    {
        public List<long> ListaPedidoDetalle { get; set; }
        public RequestPedidoDetalleRootEliminarDto()
        {
            ListaPedidoDetalle = new List<long>();
        }
    }
}
