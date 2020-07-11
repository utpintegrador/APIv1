using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoDescuento
    {
        private readonly AdTipoDescuento _adTipoDescuento = new AdTipoDescuento();

        //Descuento
        public List<TipoDescuentoObtenerDto> Obtener(RequestTipoDescuentoObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestTipoDescuentoObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdTipoDescuento";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adTipoDescuento.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<TipoDescuentoObtenerDto>();
            }
            return listado;
        }

        //Obtener Descuento
        public List<TipoDescuentoObtenerComboDto> ObtenerCombo()
        {
            var listado = _adTipoDescuento.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<TipoDescuentoObtenerComboDto>();
            }
            return listado;
        }

        //Obtener Descuento por ID
        public TipoDescuentoObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoDescuento.ObtenerPorId(id);
        }

        //Registrar Descuento
        public int Registrar(RequestTipoDescuentoRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoDescuento.Registrar(modelo, ref idNuevo);
        }

        //Modificar Descuento
        public int Modificar(RequestTipoDescuentoModificarDto modelo)
        {
            return _adTipoDescuento.Modificar(modelo);
        }

        //Eliminar Descuento
        public int Eliminar(int id)
        {
            return _adTipoDescuento.Eliminar(id);
        }
    }
}
