using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoDetalleModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponsePedidoDetalleModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
