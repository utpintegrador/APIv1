using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestNegocioUbicacionModificarDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocioUbicacion { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocio { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Titulo { get; set; }

        [MaxLength(500, ErrorMessage = "{0}: longitud máxima es de {1} caracteres")]
        public string Descripcion { get; set; }
        public bool Predeterminado { get; set; }

        public RequestNegocioUbicacionModificarDto()
        {
            Predeterminado = false;
            Titulo = string.Empty;
            Descripcion = string.Empty;
            Latitud = 0;
            Longitud = 0;
        }
    }
}
