using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDocumentoIdentificacionEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoDocumentoIdentificacionEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
