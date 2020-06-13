using Datos.Repositorio.Interaccion;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;

namespace Negocio.Repositorio.Interaccion
{
    public class LnPublicacion
    {
        private readonly AdPublicacion _adPublicacion = new AdPublicacion();
        public List<PublicacionObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            return _adPublicacion.ObtenerPorIdUsuario(idUsuario);
        }

        public PublicacionObtenerPorIdUsuarioDto ObtenerPorId(long id)
        {
            return _adPublicacion.ObtenerPorId(id);
        }

        public int Registrar(PublicacionRegistrarDto modelo, ref long idNuevo)
        {
            return _adPublicacion.Registrar(modelo, ref idNuevo);
        }

        public int Eliminar(long idPublicacion)
        {
            return _adPublicacion.Eliminar(idPublicacion);
        }
    }
}
