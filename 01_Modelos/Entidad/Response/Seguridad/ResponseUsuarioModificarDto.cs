using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseUsuarioModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
