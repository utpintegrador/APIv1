using System.Collections.Generic;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoDetalleRootModificarDto
    {
        public List<RequestPedidoDetalleModificarDto> ListaPedidoDetalle { get; set; }
        public RequestPedidoDetalleRootModificarDto()
        {
            ListaPedidoDetalle = new List<RequestPedidoDetalleModificarDto>();
        }
    }
}
