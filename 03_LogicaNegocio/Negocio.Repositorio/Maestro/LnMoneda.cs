using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnMoneda
    {
        private readonly AdMoneda _adMoneda = new AdMoneda();

        //Obtener Moneda
        public List<MonedaObtenerDto> Obtener(RequestMonedaObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestMonedaObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdMoneda";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adMoneda.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<MonedaObtenerDto>();
            }
            return listado;
        }

        //Obtener Moneda por ID
        public MonedaObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adMoneda.ObtenerPorId(id);
        }

        //Registrar Moneda
        public int Registrar(RequestMonedaRegistrarDto modelo, ref int idNuevo)
        {
            return _adMoneda.Registrar(modelo, ref idNuevo);
        }

        //Modificar Moneda
        public int Modificar(RequestMonedaModificarDto modelo)
        {
            return _adMoneda.Modificar(modelo);
        }

        //Eliminar Moneda
        public int Eliminar(int id)
        {
            return _adMoneda.Eliminar(id);
        }

        //Listar Moneda
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
