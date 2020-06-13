using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnPais
    {
        private readonly AdPais _adPais = new AdPais();

        public List<PaisObtenerDto> Obtener()
        {
            return _adPais.Obtener();
        }

        public Pais ObtenerPorId(int id)
        {
            return _adPais.ObtenerPorId(id);
        }

        public int Registrar(PaisRegistrarDto modelo, ref int idNuevo)
        {
            return _adPais.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(Pais modelo)
        {
            return _adPais.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adPais.Eliminar(id);
        }

    }
}