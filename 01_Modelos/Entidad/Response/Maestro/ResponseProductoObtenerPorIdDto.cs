using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ProductoObtenerPorIdDto Cuerpo { get; set; }
        public ResponseProductoObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
