using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioUbicacionObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public NegocioUbicacionObtenerPorIdDto Cuerpo { get; set; }
        public ResponseNegocioUbicacionObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
