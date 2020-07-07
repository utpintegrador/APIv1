using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class CategoriaModificarPrmDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(250, MinimumLength = 4, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }

        /// <summary>
        /// 1: Activo       2: Inactivo
        /// </summary>
        [Range(1, 2, ErrorMessage = "{0}: debe tener un valor entre {1} y {2}")]
        public int IdEstado { get; set; }
    }
}
