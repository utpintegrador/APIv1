using Datos.Repositorio.Interaccion;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;

namespace Negocio.Repositorio.Interaccion
{
    public class LnContacto
    {
        private readonly AdContacto _adContacto = new AdContacto();
        public int Registrar(ContactoRegistrarDto modelo, ref long idNuevo)
        {
            return _adContacto.Registrar(modelo, ref idNuevo);
        }

        public List<ContactoObtenerPendienteMatchDto> ObtenerPendienteMatch()
        {
            return _adContacto.ObtenerPendienteMatch();
        }

        public int Eliminar(long idContacto)
        {
            return _adContacto.Eliminar(idContacto);
        }

    }
}
