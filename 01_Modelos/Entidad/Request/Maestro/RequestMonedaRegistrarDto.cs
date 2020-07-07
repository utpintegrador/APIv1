using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestMonedaRegistrarDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
