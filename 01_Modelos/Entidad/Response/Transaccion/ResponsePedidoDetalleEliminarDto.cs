using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoDetalleEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponsePedidoDetalleEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
