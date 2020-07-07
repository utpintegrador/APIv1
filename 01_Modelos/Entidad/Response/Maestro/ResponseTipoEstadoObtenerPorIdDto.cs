using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoEstadoObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public TipoEstado Cuerpo { get; set; }
        public ResponseTipoEstadoObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
