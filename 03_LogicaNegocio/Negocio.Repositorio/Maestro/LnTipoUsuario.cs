using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoUsuario
    {
        private readonly AdTipoUsuario _adTipoUsuario = new AdTipoUsuario();

        //Tipo de Usuario
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

        //Obtener Tipo de Usuario por ID
        public TipoUsuarioObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoUsuario.ObtenerPorId(id);
        }

        //Registar Tipo de Usuario
        public int Registrar(RequestTipoUsuarioRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoUsuario.Registrar(modelo, ref idNuevo);
        }

        //Modificar Tipo de Usuario
        public int Modificar(RequestTipoUsuarioModificarDto modelo)
        {
            return _adTipoUsuario.Modificar(modelo);
        }

        //Eliminar Tipo de Usuario
        public int Eliminar(int id)
        {
            return _adTipoUsuario.Eliminar(id);
        }

        //Listar Tipo de Usuario
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