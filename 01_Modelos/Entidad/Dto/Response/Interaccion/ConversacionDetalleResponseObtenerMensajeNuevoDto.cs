using Entidad.Dto.Global;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Interaccion
{
    public class ConversacionDetalleResponseObtenerMensajeNuevoDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public List<ConversacionDetalleObtenerMensajeNuevoDto> Cuerpo { get; set; }
        public ConversacionDetalleResponseObtenerMensajeNuevoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
