using Entidad.Dto.Paginacion;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestNegocioUbicacionObtenerPorIdUsuarioDto: FiltroPaginacion
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }
        public long IdNegocio { get; set; }
        public string Buscar { get; set; }
        public RequestNegocioUbicacionObtenerPorIdUsuarioDto()
        {
            Buscar = string.Empty;
        }
    }
}
