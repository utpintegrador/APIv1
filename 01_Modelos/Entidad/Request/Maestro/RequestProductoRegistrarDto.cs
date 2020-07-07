using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestProductoRegistrarDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }

        [MaxLength(1000, ErrorMessage = "{0}: la longitud maxima es de {1} caracteres")]
        public string DescripcionExtendida { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdMoneda { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdCategoria { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocio { get; set; }


    }
}
