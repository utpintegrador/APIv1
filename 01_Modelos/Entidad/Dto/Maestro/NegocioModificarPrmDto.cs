using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class NegocioModificarPrmDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocio { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string DocumentoIdentificacion { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Nombre { get; set; }

        [MaxLength(500, ErrorMessage = "{0}: la longitud maxima es de {1} caracteres")]
        public string Resenia { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdTipoDocumentoIdentificacion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }

        [Range(1, 2, ErrorMessage = "{0}: debe tener un valor entre {1} y {2}")]
        public int IdEstado { get; set; }
    }
}
