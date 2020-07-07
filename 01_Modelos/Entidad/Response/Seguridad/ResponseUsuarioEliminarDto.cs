using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseUsuarioEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
