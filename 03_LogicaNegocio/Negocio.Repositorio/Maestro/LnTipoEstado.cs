using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoEstado
    {
        private readonly AdTipoEstado _adTipoEstado = new AdTipoEstado();

        public List<TipoEstadoObtenerDto> Obtener(RequestTipoEstadoObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestTipoEstadoObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdTipoEstado";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adTipoEstado.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<TipoEstadoObtenerDto>();
            }
            return listado;
        }

        public TipoEstadoObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoEstado.ObtenerPorId(id);
        }

        public int Registrar(RequestTipoEstadoRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoEstado.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestTipoEstadoModificarDto modelo)
        {
            return _adTipoEstado.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adTipoEstado.Eliminar(id);
        }

        public List<TipoEstadoObtenerComboDto> ObtenerCombo()
        {
            var listado = _adTipoEstado.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<TipoEstadoObtenerComboDto>();
            }
            return listado;
        }

    }
}
