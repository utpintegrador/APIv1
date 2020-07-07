using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Maestro
{
    public class CategoriaObtenerPrmDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
    }
}
