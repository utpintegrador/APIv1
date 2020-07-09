using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoBusqueda
    {
        private readonly AdTipoBusqueda _adTipoBusqueda = new AdTipoBusqueda();

        public List<TipoBusquedaObtenerDto> Obtener(RequestTipoBusquedaObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestTipoBusquedaObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdTipoBusqueda";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adTipoBusqueda.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<TipoBusquedaObtenerDto>();
            }
            return listado;
        }

        public List<TipoBusquedaObtenerComboDto> ObtenerCombo()
        {
            var listado = _adTipoBusqueda.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<TipoBusquedaObtenerComboDto>();
            }
            return listado;
        }

        public TipoBusquedaObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoBusqueda.ObtenerPorId(id);
        }

        public int Registrar(RequestTipoBusquedaRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoBusqueda.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestTipoBusquedaModificarDto modelo)
        {
            return _adTipoBusqueda.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adTipoBusqueda.Eliminar(id);
        }
    }
}
