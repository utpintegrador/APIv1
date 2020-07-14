using Entidad.Dto.Paginacion;

namespace Entidad.Request.Seguridad
{
    public class RequestUsuarioObtenerDto: FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoUsuario { get; set; }
        public RequestUsuarioObtenerDto()
        {
            Buscar = string.Empty;
            IdEstado = 0;
            IdTipoUsuario = 0;
        }
    }
}
