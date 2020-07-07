using Entidad.Response;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseCategoriaEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
