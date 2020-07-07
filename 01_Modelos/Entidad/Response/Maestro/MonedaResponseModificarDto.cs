using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class MonedaResponseModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public MonedaResponseModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
