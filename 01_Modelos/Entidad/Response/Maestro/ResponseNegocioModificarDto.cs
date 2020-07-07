using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseNegocioModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
