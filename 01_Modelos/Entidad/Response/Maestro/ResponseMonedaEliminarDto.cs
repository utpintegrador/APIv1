using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseMonedaEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseMonedaEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
