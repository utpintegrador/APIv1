using System.Collections.Generic;

namespace Entidad.Response.Comun
{
    public class ResponseValidacionModeloDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public object Cuerpo { get; set; }
    }
}
