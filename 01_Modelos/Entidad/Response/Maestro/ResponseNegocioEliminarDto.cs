using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseNegocioEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
