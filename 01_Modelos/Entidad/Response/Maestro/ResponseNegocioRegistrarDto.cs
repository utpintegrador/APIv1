using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public ResponseNegocioRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
