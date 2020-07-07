using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class TipoEstadoResponseEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public TipoEstadoResponseEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
