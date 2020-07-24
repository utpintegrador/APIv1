using Datos.Repositorio.Seguridad;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using System.Collections.Generic;

namespace Negocio.Repositorio.Seguridad
{
    public class LnRol
    {
        private readonly AdRol _adRol = new AdRol();
        public List<RolObtenerComboDto> ObtenerCombo()
        {
            var listado = _adRol.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<RolObtenerComboDto>();
            }
            return listado;
        }

        public List<RolObtenerDto> Obtener(RequestRolObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestRolObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdRol";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adRol.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<RolObtenerDto>();
            }
            return listado;
        }

        public RolObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adRol.ObtenerPorId(id);
        }

        public int Registrar(RequestRolRegistrarDto modelo, ref int idNuevo)
        {
            return _adRol.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestRolModificarDto modelo)
        {
            return _adRol.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adRol.Eliminar(id);
        }

        public List<RolObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            var listado = _adRol.ObtenerPorIdUsuario(idUsuario);
            if(listado == null)
            {
                listado = new List<RolObtenerPorIdUsuarioDto>();
            }
            return listado;
        }
    }
}
