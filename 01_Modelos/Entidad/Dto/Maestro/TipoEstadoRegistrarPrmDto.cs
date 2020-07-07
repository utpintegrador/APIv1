using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class TipoEstadoRegistrarPrmDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
