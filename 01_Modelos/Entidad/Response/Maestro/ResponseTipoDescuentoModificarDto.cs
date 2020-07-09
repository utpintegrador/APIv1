using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDescuentoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoDescuentoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
