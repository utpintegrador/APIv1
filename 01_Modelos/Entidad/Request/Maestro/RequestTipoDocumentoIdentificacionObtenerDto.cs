using Entidad.Dto.Paginacion;

namespace Entidad.Request.Maestro
{
    public class RequestTipoDocumentoIdentificacionObtenerDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
    }
}
