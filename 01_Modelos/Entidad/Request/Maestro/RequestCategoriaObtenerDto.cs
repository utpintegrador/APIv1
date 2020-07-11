using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestCategoriaObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
        public RequestCategoriaObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
