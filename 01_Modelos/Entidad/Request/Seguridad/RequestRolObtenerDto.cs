using Entidad.Dto.Paginacion;

namespace Entidad.Request.Seguridad
{
    public class RequestRolObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
    }
}
