using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class NegocioUbicacionResponseEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public NegocioUbicacionResponseEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
