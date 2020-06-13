using System;

namespace Entidad.Dto.Interaccion
{
    public class ConversacionObtenerDto
    {

        public long IdConversacion { get; set; }
        public string UsuarioDestino { get; set; }
        public string UltimoMensaje { get; set; }
        public string InterpretacionFecha { get; set; }
        public DateTime UltimaFecha { get; set; }
        public string UrlImagen { get; set; }

    }
}
