using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoEstadoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoEstadoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
