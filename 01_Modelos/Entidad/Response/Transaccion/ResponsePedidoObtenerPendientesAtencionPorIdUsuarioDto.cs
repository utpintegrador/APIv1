using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoObtenerPendientesAtencionPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoObtenerPendientesAtencionPorIdUsuarioDto> Cuerpo { get; set; }
        public long CantidadTotalRegistros { get; set; }
        public ResponsePedidoObtenerPendientesAtencionPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
