using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoEstadoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoEstadoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
