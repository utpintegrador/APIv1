using Datos.Repositorio.Perfil;
using Entidad.Dto.Perfil;
using Entidad.Entidad.Perfil;
using System.Collections.Generic;

namespace Negocio.Repositorio.Perfil
{
    public class LnAlbumImagen
    {
        private readonly AdAlbumImagen _adAlbumImagen = new AdAlbumImagen();
        public List<AlbumImagenObtenerDto> ObtenerPorIdPerfil(long id)
        {
            return _adAlbumImagen.ObtenerPorIdPerfil(id);
        }

        public AlbumImagen ObtenerPorId(long id)
        {
            return _adAlbumImagen.ObtenerPorId(id);
        }

        public int Registrar(AlbumImagenRegistrarDto modelo, ref long idNuevo)
        {
            return _adAlbumImagen.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(AlbumImagen modelo)
        {
            return _adAlbumImagen.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adAlbumImagen.Eliminar(id);
        }

        public int EliminarImagen(long id)
        {
            return _adAlbumImagen.EliminarImagen(id);
        }

        public int ModificarImagen(AlbumImagen modelo)
        {
            return _adAlbumImagen.ModificarImagen(modelo);
        }
    }
}
