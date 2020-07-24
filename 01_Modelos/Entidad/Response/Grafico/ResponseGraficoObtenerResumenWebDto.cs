using Entidad.Dto.Grafico;
using System.Collections.Generic;

namespace Entidad.Response.Grafico
{
    public class ResponseGraficoObtenerResumenWebDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public GraficoObtenerResumenDto Cuerpo { get; set; }
        public ResponseGraficoObtenerResumenWebDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
