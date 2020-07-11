using Entidad.Dto.Paginacion;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoObtenerPorIdNegocioCompradorDto : FiltroPaginacion
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocioComprador { get; set; }
        public string Buscar { get; set; }
        public RequestPedidoObtenerPorIdNegocioCompradorDto()
        {
            Buscar = string.Empty;
        }
    }
}
