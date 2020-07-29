using Datos.Repositorio.Seguimiento;
using Entidad.Dto.Seguimiento;
using System.Collections.Generic;

namespace Negocio.Repositorio.Seguimiento
{
    public class LnPedidoControlEstado
    {
        private readonly AdPedidoControlEstado _adPedidoControlEstado = new AdPedidoControlEstado();
        public List<PedidoControlEstadoObtenerPorIdPedidoDto> ObtenerPorIdPedido(long idPedido)
        {
            var listado = _adPedidoControlEstado.ObtenerPorIdPedido(idPedido);
            if (listado == null)
            {
                listado = new List<PedidoControlEstadoObtenerPorIdPedidoDto>();
            }
            return listado;
        }
    }
}
