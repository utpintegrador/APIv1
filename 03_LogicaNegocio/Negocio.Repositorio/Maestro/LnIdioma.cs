using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnIdioma
    {
        private readonly AdIdioma _adIdioma = new AdIdioma();

        public List<IdiomaObtenerDto> Obtener()
        {
            return _adIdioma.Obtener();
        }

        public Idioma ObtenerPorId(int id)
        {
            return _adIdioma.ObtenerPorId(id);
        }

        public int Registrar(IdiomaRegistrarDto modelo, ref int idNuevo)
        {
            return _adIdioma.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(Idioma modelo)
        {
            return _adIdioma.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adIdioma.Eliminar(id);
        }

    }
}
