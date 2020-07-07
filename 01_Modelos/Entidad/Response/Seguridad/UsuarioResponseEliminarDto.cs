using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class UsuarioResponseEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public UsuarioResponseEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
