using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseEstadoObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public Estado Cuerpo { get; set; }
        public ResponseEstadoObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
