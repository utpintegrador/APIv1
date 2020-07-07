using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Seguridad
{
    public class RequestUsuarioModificarImagenMetodo1Dto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public string ExtensionSinPunto { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        public byte[] ArchivoBytes { get; set; }
    }
}
