using Datos.Repositorio.Perfil;
using Entidad.Dto.Perfil;
using Entidad.Entidad.Perfil;
using System.Collections.Generic;

namespace Negocio.Repositorio.Perfil
{
    public class LnImagen
    {
        private readonly AdImagen _adImagen = new AdImagen();
        public List<ImagenObtenerDto> ObtenerPorIdAlbumImagen(long id)
        {
            return _adImagen.ObtenerPorIdAlbumImagen(id);
        }

        public Imagen ObtenerPorId(long id)
        {
            return _adImagen.ObtenerPorId(id);
        }

        public int Registrar(ImagenRegistrarDto modelo, ref long idNuevo)
        {
            return _adImagen.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(Imagen modelo)
        {
            return _adImagen.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adImagen.Eliminar(id);
        }
    }
}
