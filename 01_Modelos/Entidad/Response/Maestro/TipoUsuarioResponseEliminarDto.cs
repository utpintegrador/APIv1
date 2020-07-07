using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class TipoUsuarioResponseEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public TipoUsuarioResponseEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
