using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnInteresGenero
    {
        private readonly AdInteresGenero _adInteresGenero = new AdInteresGenero();

        public List<InteresGeneroObtenerDto> Obtener()
        {
            return _adInteresGenero.Obtener();
        }

        public InteresGenero ObtenerPorId(int id)
        {
            return _adInteresGenero.ObtenerPorId(id);
        }

        public int Registrar(InteresGeneroRegistrarDto modelo, ref int idNuevo)
        {
            return _adInteresGenero.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(InteresGenero modelo)
        {
            return _adInteresGenero.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adInteresGenero.Eliminar(id);
        }

    }
}