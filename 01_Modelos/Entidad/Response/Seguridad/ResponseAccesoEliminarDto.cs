using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseAccesoEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseAccesoEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
