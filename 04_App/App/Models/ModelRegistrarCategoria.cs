using Microsoft.AspNetCore.Http;

namespace App.Models
{
    public class ModelRegistrarCategoria
    {
        public IFormFile Archivo { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
    }
}
