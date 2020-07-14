using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoEliminarMasivoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseProductoEliminarMasivoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
