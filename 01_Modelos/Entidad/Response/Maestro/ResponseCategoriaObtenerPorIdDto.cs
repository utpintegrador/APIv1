using System.Collections.Generic;
using Entidad.Dto.Maestro;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public CategoriaObtenerPorIdDto Cuerpo { get; set; }
        public ResponseCategoriaObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
