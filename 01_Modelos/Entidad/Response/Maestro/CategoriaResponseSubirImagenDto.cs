using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class CategoriaResponseSubirImagenDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public CategoriaResponseSubirImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}
