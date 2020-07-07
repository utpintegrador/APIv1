using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoUsuarioObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public TipoUsuario Cuerpo { get; set; }
        public ResponseTipoUsuarioObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
