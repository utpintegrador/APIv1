using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestEstadoObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdTipoEstado { get; set; }
        public RequestEstadoObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
