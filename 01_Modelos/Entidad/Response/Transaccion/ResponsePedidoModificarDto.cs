using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponsePedidoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
