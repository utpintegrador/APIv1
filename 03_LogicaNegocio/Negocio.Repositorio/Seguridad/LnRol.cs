using Datos.Repositorio.Seguridad;
using Entidad.Dto.Seguridad;
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
    }
}
