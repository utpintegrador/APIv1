using Datos.Repositorio.Seguridad;
using Entidad.Dto.Seguridad;
using Entidad.Entidad.Seguridad;
using System.Collections.Generic;

namespace Negocio.Repositorio.Seguridad
{
    public class LnUsuario
    {
        private readonly AdUsuario _adUsuario = new AdUsuario();
        public UsuarioLoginDto ObtenerPorLogin(UsuarioCredencialesDto modelo)
        {
            return _adUsuario.ObtenerPorLogin(modelo);
        }

        public List<UsuarioObtenerDto> Obtener(UsuarioObtenerFiltroDto filtro)
        {
            if (filtro == null) filtro = new UsuarioObtenerFiltroDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdUsuario";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            return _adUsuario.Obtener(filtro);
        }

        public Usuario ObtenerPorId(long id)
        {
            return _adUsuario.ObtenerPorId(id);
        }

        public int Registrar(UsuarioRegistrarDto modelo, ref long idNuevo)
        {
            return _adUsuario.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(UsuarioModificarDto modelo)
        {
            return _adUsuario.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adUsuario.Eliminar(id);
        }

        public int ModificarContrasenia(UsuarioCambioContraseniaDto modelo)
        {
            return _adUsuario.ModificarContrasenia(modelo);
        }

        public int ModificarUrlImagenPorIdUsuario(long idUsuario, string url)
        {
            return _adUsuario.ModificarUrlImagenPorIdUsuario(idUsuario, url);
        }

        public UsuarioObtenerUrlImagenDto ObtenerUrlImagenPorId(long id)
        {
            return _adUsuario.ObtenerUrlImagenPorId(id);
        }

        public int EliminarUrlImagen(long id)
        {
            return _adUsuario.EliminarUrlImagen(id);

        }

    }
}
