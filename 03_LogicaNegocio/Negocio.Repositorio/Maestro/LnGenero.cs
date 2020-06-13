using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnGenero
    {
        private readonly AdGenero _adGenero = new AdGenero();

        public List<GeneroObtenerDto> Obtener()
        {
            return _adGenero.Obtener();
        }

        public Genero ObtenerPorId(int id)
        {
            return _adGenero.ObtenerPorId(id);
        }

        public int Registrar(GeneroRegistrarDto modelo, ref int idNuevo)
        {
            return _adGenero.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(Genero modelo)
        {
            return _adGenero.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adGenero.Eliminar(id);
        }
    }
}
