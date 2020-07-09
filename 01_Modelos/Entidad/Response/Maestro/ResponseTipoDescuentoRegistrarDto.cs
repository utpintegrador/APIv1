using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDescuentoRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public int IdGenerado { get; set; }
        public ResponseTipoDescuentoRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
