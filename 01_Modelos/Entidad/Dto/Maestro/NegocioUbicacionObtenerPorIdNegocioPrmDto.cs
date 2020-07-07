using Entidad.Dto.Paginacion;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class NegocioUbicacionObtenerPorIdNegocioPrmDto : FiltroPaginacion
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocio { get; set; }
        public string Buscar { get; set; }
    }
}
