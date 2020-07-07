using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseEstadoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseEstadoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
