using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using Entidad.Request.Maestro;
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

        public int Registrar(RequestMonedaRegistrarDto modelo, ref int idNuevo)
        {
            return _adMoneda.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestMonedaModificarDto modelo)
        {
            return _adMoneda.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adMoneda.Eliminar(id);
        }

        public List<MonedaObtenerComboDto> ObtenerCombo()
        {
            var listado = _adMoneda.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<MonedaObtenerComboDto>();
            }
            return listado;
        }
    }
}
