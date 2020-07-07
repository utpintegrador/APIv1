using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseMonedaModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseMonedaModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
