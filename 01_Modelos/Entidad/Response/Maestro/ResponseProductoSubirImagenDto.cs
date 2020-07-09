using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoSubirImagenDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public ResponseProductoSubirImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}
