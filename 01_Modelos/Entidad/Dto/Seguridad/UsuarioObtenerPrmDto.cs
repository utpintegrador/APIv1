using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Seguridad
{
    public class UsuarioObtenerPrmDto: FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
    }
}
