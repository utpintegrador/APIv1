using Entidad.Response;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class ResponseEstadoRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public int IdGenerado { get; set; }
        public ResponseEstadoRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
