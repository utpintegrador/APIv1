using Microsoft.AspNetCore.Http;

namespace App.Models
{
    public class ModelAwsS3RegistrarUsuario
    {
        public IFormFile Archivo { get; set; }
        public string Accion { get; set; }
        public long IdUsuario { get; set; }
    }
}
