using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestTipoDescuentoObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public RequestTipoDescuentoObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
