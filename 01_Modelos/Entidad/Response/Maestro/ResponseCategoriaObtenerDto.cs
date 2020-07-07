using Entidad.Response;
using Entidad.Dto.Maestro;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseCategoriaObtenerDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<CategoriaObtenerDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponseCategoriaObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
