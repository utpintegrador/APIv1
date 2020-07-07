using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Correo
{
    public class RecuperacionContraseniaRegistrarFiltroDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        //[EmailAddress(ErrorMessage = "{0}: el valor ingresado no cumple con un formato de correo válido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}: el valor ingresado no cumple con un formato de correo válido")]
        public string CorreoElectronico { get; set; }
    }
}
