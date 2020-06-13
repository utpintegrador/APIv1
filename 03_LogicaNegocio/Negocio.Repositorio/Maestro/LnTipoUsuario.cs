using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoUsuario
    {
        private readonly AdTipoUsuario _adTipoUsuario = new AdTipoUsuario();

        public List<TipoUsuarioObtenerDto> Obtener()
        {
            return _adTipoUsuario.Obtener();
        }

        public TipoUsuario ObtenerPorId(int id)
        {
            return _adTipoUsuario.ObtenerPorId(id);
        }

        public int Registrar(TipoUsuarioRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoUsuario.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(TipoUsuario modelo)
        {
            return _adTipoUsuario.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adTipoUsuario.Eliminar(id);
        }

    }
}