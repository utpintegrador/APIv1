using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestTipoBusquedaObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
    }
}
