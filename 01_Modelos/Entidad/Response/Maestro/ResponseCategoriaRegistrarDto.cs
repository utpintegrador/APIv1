using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public int IdGenerado { get; set; }
        public ResponseCategoriaRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
