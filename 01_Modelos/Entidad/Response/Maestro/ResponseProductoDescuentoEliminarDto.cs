using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoDescuentoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseProductoDescuentoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
