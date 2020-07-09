using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Seguridad
{
    public class RequestAccesoModificarDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public int IdAcceso { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string UrlAcceso { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Icono { get; set; }

        public int? IdAccesoPadre { get; set; }
    }
}
