using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDocumentoIdentificacionModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoDocumentoIdentificacionModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
