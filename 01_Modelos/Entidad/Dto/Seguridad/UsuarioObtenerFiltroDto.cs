using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Seguridad
{
    public class UsuarioObtenerFiltroDto: FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
    }
}
