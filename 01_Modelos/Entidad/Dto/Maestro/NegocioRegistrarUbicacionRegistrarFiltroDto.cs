using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class NegocioRegistrarUbicacionRegistrarFiltroDto
    {
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Titulo { get; set; }

        [MaxLength(500, ErrorMessage = "{0}: longitud máxima es de {1} caracteres")]
        public string Descripcion { get; set; }
        public int Predeterminado { get; set; }

        public NegocioRegistrarUbicacionRegistrarFiltroDto()
        {
            Predeterminado = 0;
            Titulo = string.Empty;
            Descripcion = string.Empty;
            Latitud = 0;
            Longitud = 0;
        }

    }
}
