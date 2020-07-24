using Entidad.Dto.Paginacion;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestNegocioObtenerCercanosDto //: FiltroPaginacion
    {
        public string Buscar { get; set; }
        public long UbicacionLongitudInicio { get; set; }
        public long UbicacionLatitudInicio { get; set; }
        public int CantidadKilometros { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }
        public RequestNegocioObtenerCercanosDto()
        {
            Buscar = string.Empty;
        }
    }
}
