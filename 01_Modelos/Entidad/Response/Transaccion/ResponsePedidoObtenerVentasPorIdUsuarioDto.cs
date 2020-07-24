using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoObtenerVentasPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoObtenerVentasPorIdUsuarioDto> Cuerpo { get; set; }
        public long CantidadTotalRegistros { get; set; }
        public ResponsePedidoObtenerVentasPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
