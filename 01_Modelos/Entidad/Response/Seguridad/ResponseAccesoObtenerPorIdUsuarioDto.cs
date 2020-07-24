using Entidad.Dto.Seguridad;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseAccesoObtenerPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public AccesoRootDto Cuerpo { get; set; }
        public ResponseAccesoObtenerPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
