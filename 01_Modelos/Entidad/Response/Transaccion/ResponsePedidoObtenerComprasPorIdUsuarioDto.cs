using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoObtenerComprasPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoObtenerComprasPorIdUsuarioDto> Cuerpo { get; set; }
        public long CantidadTotalRegistros { get; set; }
        public ResponsePedidoObtenerComprasPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
