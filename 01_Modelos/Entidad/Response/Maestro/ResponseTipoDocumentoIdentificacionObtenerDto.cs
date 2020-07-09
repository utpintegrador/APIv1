using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoDocumentoIdentificacionObtenerDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<TipoDocumentoIdentificacionObtenerDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponseTipoDocumentoIdentificacionObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
