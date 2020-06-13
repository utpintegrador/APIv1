using System;

namespace Entidad.Dto.Interaccion
{
    public class ConversacionDetalleObtenerDto
    {
        public long IdConversacionDetalle { get; set; }
        public long IdUsuarioEmisor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEstadoMensaje { get; set; }
        public string EstadoMensaje { get; set; }

    }
}
