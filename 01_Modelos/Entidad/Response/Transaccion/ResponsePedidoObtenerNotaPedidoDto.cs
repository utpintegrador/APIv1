using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoObtenerNotaPedidoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public PedidoObtenerNotaPedidoDto Cuerpo { get; set; }
        public ResponsePedidoObtenerNotaPedidoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
