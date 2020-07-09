using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDescuentoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoDescuentoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
