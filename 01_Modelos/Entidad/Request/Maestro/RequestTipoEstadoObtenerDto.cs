using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestTipoEstadoObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
    }
}
