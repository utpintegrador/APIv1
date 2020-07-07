using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioUbicacionEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseNegocioUbicacionEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
