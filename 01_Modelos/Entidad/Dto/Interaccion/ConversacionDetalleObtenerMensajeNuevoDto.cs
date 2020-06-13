using System;

namespace Entidad.Dto.Interaccion
{
    public class ConversacionDetalleObtenerMensajeNuevoDto
    {
        public long IdConversacionDetalle { get; set; }
        public long IdUsuarioEmisor { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
