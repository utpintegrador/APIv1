using System;

namespace Entidad.Entidad.Perfil
{
    public class AlbumImagen
    {
        public long IdAlbumImagen { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagenAlbum { get; set; }
        //public DateTime FechaRegistro { get; set; }
        public long IdPerfil { get; set; }
    }
}
