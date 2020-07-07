using Entidad.Response;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public Categoria Cuerpo { get; set; }
        public ResponseCategoriaObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
