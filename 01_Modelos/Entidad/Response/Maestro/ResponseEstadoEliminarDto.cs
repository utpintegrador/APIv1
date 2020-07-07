using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseEstadoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseEstadoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
