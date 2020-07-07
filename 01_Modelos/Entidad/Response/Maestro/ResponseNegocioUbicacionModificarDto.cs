using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioUbicacionModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseNegocioUbicacionModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
