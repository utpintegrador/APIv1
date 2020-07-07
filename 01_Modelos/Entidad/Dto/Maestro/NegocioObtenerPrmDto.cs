using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Maestro
{
    public class NegocioObtenerPrmDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
        public long IdUsuario { get; set; }
    }
}
