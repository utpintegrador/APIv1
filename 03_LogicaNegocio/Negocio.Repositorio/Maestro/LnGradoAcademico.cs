using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;


namespace Negocio.Repositorio.Maestro
{
    public class LnGradoAcademico
    {
        private readonly AdGradoAcademico _adGradoAcademico = new AdGradoAcademico();

        public List<GradoAcademicoObtenerDto> Obtener()
        {
            return _adGradoAcademico.Obtener();
        }

        public GradoAcademico ObtenerPorId(int id)
        {
            return _adGradoAcademico.ObtenerPorId(id);
        }

        public int Registrar(GradoAcademicoRegistrarDto modelo, ref int idNuevo)
        {
            return _adGradoAcademico.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(GradoAcademico modelo)
        {
            return _adGradoAcademico.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adGradoAcademico.Eliminar(id);
        }

    }
}
