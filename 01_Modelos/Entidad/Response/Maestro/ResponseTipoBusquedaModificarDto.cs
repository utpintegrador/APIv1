using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoBusquedaModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoBusquedaModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
