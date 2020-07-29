using Entidad.Dto.Seguimiento;
using System.Collections.Generic;

namespace Entidad.Response.Seguimiento
{
    public class ResponsePedidoControlEstadoObtenerPorIdPedidoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoControlEstadoObtenerPorIdPedidoDto> Cuerpo { get; set; }
        public ResponsePedidoControlEstadoObtenerPorIdPedidoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
