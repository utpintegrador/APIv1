using System.Collections.Generic;

namespace Entidad.Dto.Maestro
{
    public class ProductoAtributoDto
    {
        public long IdProducto { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionExtendida { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdCategoria { get; set; }
        public long IdNegocio { get; set; }
        public int IdEstado { get; set; }
        public List<ProductoAtributoDescuentoDto> ListaDescuento { get; set; }
        public List<ProductoAtributoImagenDto> ListaImagen { get; set; }
        public ProductoAtributoDto()
        {
            ListaDescuento = new List<ProductoAtributoDescuentoDto>();
            ListaImagen = new List<ProductoAtributoImagenDto>();
        }

    }
}
