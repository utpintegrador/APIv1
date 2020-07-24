using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioUbicacionObtenerPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<NegocioUbicacionObtenerPorIdUsuarioDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponseNegocioUbicacionObtenerPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
