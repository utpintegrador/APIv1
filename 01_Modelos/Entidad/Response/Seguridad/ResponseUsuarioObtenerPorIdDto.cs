using Entidad.Entidad.Seguridad;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public Usuario Cuerpo { get; set; }
        public ResponseUsuarioObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
