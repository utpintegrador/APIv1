using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseEstadoObtenerPorIdTipoEstadoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<EstadoObtenerPorIdTipoEstadoDto> Cuerpo { get; set; }
        public ResponseEstadoObtenerPorIdTipoEstadoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
