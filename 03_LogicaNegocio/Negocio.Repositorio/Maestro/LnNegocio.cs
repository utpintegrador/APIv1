using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnNegocio
    {
        private readonly AdNegocio _adNegocio = new AdNegocio();
        public List<NegocioObtenerDto> Obtener()
        {
            return _adNegocio.Obtener();
        } 

        public NegocioEnt ObtenerPorId(int id)
        {
            return _adNegocio.ObtenerPorId(id);
        }

        public int Registrar(NegocioRegistrarDto modelo, ref int idNuevo)
        {
            return _adNegocio.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(NegocioEnt modelo)
        {
            return _adNegocio.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adNegocio.Eliminar(id);
        }

    }
}
