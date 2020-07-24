using Entidad.Dto.Grafico;
using System.Collections.Generic;

namespace Entidad.Response.Grafico
{
    public class ResponseGraficoObtenerResumenVentasDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<GraficoObtenerResumenVentasDto> Cuerpo { get; set; }
        public ResponseGraficoObtenerResumenVentasDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
