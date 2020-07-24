using Entidad.Dto.Grafico;
using System.Collections.Generic;

namespace Entidad.Response.Grafico
{
    public class ResponseGraficoObtenerResumenComprasDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<GraficoObtenerResumenComprasDto> Cuerpo { get; set; }
        public ResponseGraficoObtenerResumenComprasDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
