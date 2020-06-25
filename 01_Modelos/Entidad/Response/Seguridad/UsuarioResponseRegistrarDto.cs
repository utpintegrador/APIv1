using Entidad.Response;
using Entidad.Entidad.Seguridad;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Seguridad
{
    public class UsuarioResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
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
