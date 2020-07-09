using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoUsuario
    {
        private readonly AdTipoUsuario _adTipoUsuario = new AdTipoUsuario();

        public List<TipoUsuarioObtenerDto> Obtener(RequestTipoUsuarioObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestTipoUsuarioObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdTipoUsuario";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adTipoUsuario.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<TipoUsuarioObtenerDto>();
            }
            return listado;
        }

        public TipoUsuarioObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoUsuario.ObtenerPorId(id);
        }

        public int Registrar(RequestTipoUsuarioRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoUsuario.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestTipoUsuarioModificarDto modelo)
        {
            return _adTipoUsuario.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adTipoUsuario.Eliminar(id);
        }

        public List<TipoUsuarioObtenerComboDto> ObtenerCombo()
        {
            var listado = _adTipoUsuario.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<TipoUsuarioObtenerComboDto>();
            }
            return listado;
        }

    }
}