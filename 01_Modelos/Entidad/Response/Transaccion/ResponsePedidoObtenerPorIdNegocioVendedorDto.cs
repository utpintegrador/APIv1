using Entidad.Dto.Transaccion;
using System.Collections.Generic;

namespace Entidad.Response.Transaccion
{
    public class ResponsePedidoObtenerPorIdNegocioVendedorDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<PedidoObtenerPorIdNegocioVendedorDto> Cuerpo { get; set; }
        public long CantidadTotalRegistros { get; set; }
        public ResponsePedidoObtenerPorIdNegocioVendedorDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
