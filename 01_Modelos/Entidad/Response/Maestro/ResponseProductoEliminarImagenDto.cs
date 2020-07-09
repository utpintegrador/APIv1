using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoEliminarImagenDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public ResponseProductoEliminarImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}
