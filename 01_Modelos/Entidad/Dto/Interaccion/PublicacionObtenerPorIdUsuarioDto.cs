using System;

namespace Entidad.Dto.Interaccion
{
    public class PublicacionObtenerPorIdUsuarioDto
    {
        public long IdPublicacion { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Texto { get; set; }
        public string FechaCreacion { get; set; }
        public string Origen { get; set; }
        public int IdPrivacidad { get; set; }
        public string DescripcionPrivacidad { get; set; }
        public int CantidadMeGusta { get; set; }
    }
}
