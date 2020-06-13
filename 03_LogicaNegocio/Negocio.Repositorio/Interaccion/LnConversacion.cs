using Datos.Repositorio.Interaccion;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;

namespace Negocio.Repositorio.Interaccion
{
    public class LnConversacion
    {
        private readonly AdConversacion _adConversacion = new AdConversacion();
        public List<ConversacionObtenerDto> ObtenerPorIdUsuario(long idUsuario)
        {
            return _adConversacion.ObtenerPorIdUsuario(idUsuario);
        }
    }
}
