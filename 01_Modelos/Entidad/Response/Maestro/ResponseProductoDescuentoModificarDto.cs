using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoDescuentoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseProductoDescuentoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
