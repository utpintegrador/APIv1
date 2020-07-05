using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Maestro
{
    public class CategoriaObtenerFiltroDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
    }
}
