using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseProductoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
