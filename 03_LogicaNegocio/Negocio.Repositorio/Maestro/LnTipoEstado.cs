using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoEstado
    {
        private readonly AdTipoEstado _adTipoEstado = new AdTipoEstado();

        public List<TipoEstadoObtenerDto> Obtener()
        {
            return _adTipoEstado.Obtener();
        }

        public TipoEstado ObtenerPorId(int id)
        {
            return _adTipoEstado.ObtenerPorId(id);
        }

        public int Registrar(TipoEstadoRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoEstado.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(TipoEstado modelo)
        {
            return _adTipoEstado.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adTipoEstado.Eliminar(id);
        }

    }
}
