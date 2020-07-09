using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoModificarEstadoPorParteDeCompradorDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponsePedidoModificarEstadoPorParteDeCompradorDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
