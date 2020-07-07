using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class UsuarioResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public UsuarioResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
