using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioSubirImagenDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public ResponseUsuarioSubirImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}
