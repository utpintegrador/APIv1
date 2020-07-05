using Entidad.Dto.Paginacion;

namespace Entidad.Dto.Maestro
{
    public class NegocioObtenerCercanosFiltroDto : FiltroPaginacion
    {
        public string Buscar { get; set; }
        public int CantidadKilometros { get; set; }
        public long IdUsuario { get; set; }
    }
}
