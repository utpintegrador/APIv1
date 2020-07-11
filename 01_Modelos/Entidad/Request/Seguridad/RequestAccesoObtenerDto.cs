using Entidad.Dto.Paginacion;

namespace Entidad.Request.Seguridad
{
    public class RequestAccesoObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public RequestAccesoObtenerDto()
        {
            Buscar = string.Empty;
        }
    }
}
