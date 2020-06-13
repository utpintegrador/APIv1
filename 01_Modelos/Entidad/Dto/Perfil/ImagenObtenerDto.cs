using System;

namespace Entidad.Dto.Perfil
{
    public class ImagenObtenerDto
    {
        public long IdImagen { get; set; }
        public string Url { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string DescripcionAlbumImagen { get; set; }
    }
}
