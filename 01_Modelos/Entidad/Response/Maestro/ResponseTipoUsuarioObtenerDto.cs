using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoUsuarioObtenerDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<TipoUsuarioObtenerDto> Cuerpo { get; set; }
        public ResponseTipoUsuarioObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
