using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoDetalleObtenerPorIdPedidoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoDetalleObtenerPorIdPedidoDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponsePedidoDetalleObtenerPorIdPedidoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
