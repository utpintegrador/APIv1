using System.Collections.Generic;

namespace Entidad.Dto.Transaccion
{
    public class PedidoObtenerNotaPedidoDto
    {
        public long IdPedido { get; set; }
        public string NombreVendedor { get; set; }
        public string TelefonoVendedor { get; set; }
        public string DocumentoIdentificacionVendedor { get; set; }
        public string DescripcionTipoDocumentoIdentificacionVendedor { get; set; }
        public string NombreComprador { get; set; }
        public string TelefonoComprador { get; set; }
        public string DocumentoIdentificacionComprador { get; set; }
        public string DescripcionTipoDocumentoIdentificacionComprador { get; set; }
        public string Direccion { get; set; }
        public string DescripcionMoneda { get; set; }
        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; }
        public decimal Total { get; set; }
        public string FechaRegistro { get; set; }
        public List<PedidoAtributoDetalleDto> ListaDetalle { get; set; }
        public PedidoObtenerNotaPedidoDto()
        {
            ListaDetalle = new List<PedidoAtributoDetalleDto>();
        }
    }
}
