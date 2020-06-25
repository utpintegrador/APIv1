namespace Entidad.Dto.Paginacion
{
    public class FiltroPaginacion
    {
        public int NumeroPagina { get; set; }
        public int CantidadRegistros { get; set; }
        public string ColumnaOrden { get; set; }
        public string DireccionOrden { get; set; }
    }
}
