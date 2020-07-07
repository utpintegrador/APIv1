using Entidad.Dto.Paginacion;

namespace Entidad.Request.Seguridad
{
    public class RequestUsuarioObtenerDto: FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
    }
}
