using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestTipoUsuarioObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public RequestTipoUsuarioObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
