using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoUsuarioEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoUsuarioEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
