using Entidad.Response;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Interaccion
{
    public class ConversacionResponseObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public List<ConversacionObtenerDto> Cuerpo { get; set; }
        public ConversacionResponseObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
