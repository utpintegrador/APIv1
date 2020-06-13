using System;

namespace Entidad.Dto.Perfil
{
    public class AlbumImagenObtenerDto
    {
        public long IdAlbumImagen { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagenAlbum { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
