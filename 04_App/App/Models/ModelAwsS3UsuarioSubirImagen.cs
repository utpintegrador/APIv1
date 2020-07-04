using Microsoft.AspNetCore.Http;

namespace App.Models
{
    public class ModelAwsS3UsuarioSubirImagen
    {
        public long IdUsuario { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
