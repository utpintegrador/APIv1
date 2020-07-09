using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestTipoDocumentoIdentificacionRegistrarDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
