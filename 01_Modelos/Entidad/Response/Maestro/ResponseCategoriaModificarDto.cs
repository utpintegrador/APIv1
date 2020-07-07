using Entidad.Response;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseCategoriaModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
