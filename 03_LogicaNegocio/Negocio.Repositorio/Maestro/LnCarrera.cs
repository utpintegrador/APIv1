using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnCarrera
    {
        private readonly AdCarrera _adCarrera = new AdCarrera();

        public List<CarreraObtenerDto> Obtener()
        {
            return _adCarrera.Obtener();
        }

        public Carrera ObtenerPorId(int id)
        {
            return _adCarrera.ObtenerPorId(id);
        }

        public int Registrar(CarreraRegistrarDto modelo, ref int idNuevo)
        {
            return _adCarrera.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(Carrera modelo)
        {
            return _adCarrera.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adCarrera.Eliminar(id);
        }

    }
}
