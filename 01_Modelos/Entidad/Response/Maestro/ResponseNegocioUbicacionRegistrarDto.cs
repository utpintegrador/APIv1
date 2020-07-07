using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioUbicacionRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public ResponseNegocioUbicacionRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
