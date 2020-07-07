using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseNegocioObtenerCercanosDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<NegocioObtenerCercanosDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponseNegocioObtenerCercanosDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
