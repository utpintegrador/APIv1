using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestMonedaObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public RequestMonedaObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
