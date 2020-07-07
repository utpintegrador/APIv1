using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnMoneda
    {
        private readonly AdMoneda _adMoneda = new AdMoneda();
        public List<MonedaObtenerDto> Obtener()
        {
            return _adMoneda.Obtener();
        }

        public Moneda ObtenerPorId(int id)
        {
            return _adMoneda.ObtenerPorId(id);
        }

        public int Registrar(MonedaRegistrarPrmDto modelo, ref int idNuevo)
        {
            return _adMoneda.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(MonedaModificarPrmDto modelo)
        {
            return _adMoneda.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adMoneda.Eliminar(id);
        }
    }
}
