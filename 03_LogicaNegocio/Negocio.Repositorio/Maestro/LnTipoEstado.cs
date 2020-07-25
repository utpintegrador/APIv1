using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoEstado
    {
        private readonly AdTipoEstado _adTipoEstado = new AdTipoEstado();

        //Tipo de Estado
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

        //Obtener Tipo de Estado por ID
        public TipoEstadoObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoEstado.ObtenerPorId(id);
        }

        //Registrar Tipo de Estado
        public int Registrar(RequestTipoEstadoRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoEstado.Registrar(modelo, ref idNuevo);
        }

        //Modificar Tipo de Estado
        public int Modificar(RequestTipoEstadoModificarDto modelo)
        {
            return _adTipoEstado.Modificar(modelo);
        }

        //Eliminar Tipo de Estado
        public int Eliminar(int id)
        {
            return _adTipoEstado.Eliminar(id);
        }

        //Listar Tipo de Estado
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
