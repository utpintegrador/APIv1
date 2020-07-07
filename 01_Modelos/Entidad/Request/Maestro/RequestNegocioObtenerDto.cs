using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestNegocioObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
        public long IdUsuario { get; set; }
    }
}
