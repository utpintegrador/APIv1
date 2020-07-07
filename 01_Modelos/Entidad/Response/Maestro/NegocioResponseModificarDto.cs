using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class NegocioResponseModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public NegocioResponseModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
