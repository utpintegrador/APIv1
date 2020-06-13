using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnInteresSentimental
    {
        private readonly AdInteresSentimental _adInteresSentimental = new AdInteresSentimental();

        public List<InteresSentimentalObtenerDto> Obtener()
        {
            return _adInteresSentimental.Obtener();
        }

        public InteresSentimental ObtenerPorId(int id)
        {
            return _adInteresSentimental.ObtenerPorId(id);
        }

        public int Registrar(InteresSentimentalRegistrarDto modelo, ref int idNuevo)
        {
            return _adInteresSentimental.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(InteresSentimental modelo)
        {
            return _adInteresSentimental.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adInteresSentimental.Eliminar(id);
        }

    }
}
