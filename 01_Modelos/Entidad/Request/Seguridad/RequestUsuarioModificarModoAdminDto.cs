using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Seguridad
{
    public class RequestUsuarioModificarModoAdminDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}: el valor ingresado no cumple con un formato de correo válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Apellido { get; set; }

        [Range(3, 6, ErrorMessage = "{0}: debe tener un valor entre {1} y {2}")]
        public int IdEstado { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public int IdTipoUsuario { get; set; }
    }
}
