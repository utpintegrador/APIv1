using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class NegocioUbicacionResponseModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public NegocioUbicacionResponseModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
