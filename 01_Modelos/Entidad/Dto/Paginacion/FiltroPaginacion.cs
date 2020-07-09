using System.ComponentModel;

namespace Entidad.Dto.Paginacion
{
    public class FiltroPaginacion
    {
        [DefaultValue(1)]
        public int NumeroPagina { get; set; }
        [DefaultValue(10)]
        public int CantidadRegistros { get; set; }
        [DefaultValue("Id")]
        public string ColumnaOrden { get; set; }
        [DefaultValue("desc")]
        public string DireccionOrden { get; set; }
    }
}
