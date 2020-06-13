using Datos.Repositorio.Interaccion;
using Entidad.Dto.Interaccion;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Repositorio.Interaccion
{
    public class LnConversacionDetalle
    {
        private readonly AdConversacionDetalle _adConversacionDetalle = new AdConversacionDetalle();
        public List<ConversacionDetalleObtenerDto> ObtenerPorIdConversacion(long idConversacion)
        {
            return _adConversacionDetalle.ObtenerPorIdConversacion(idConversacion);
        }

        public int Registrar(ConversacionDetalleRegistrarDto modelo, ref long idNuevo)
        {
            return _adConversacionDetalle.Registrar(modelo, ref idNuevo);
        }

        public List<ConversacionDetalleObtenerMensajeNuevoDto> ObtenerMensajesNuevos(long idUsuario, long idConversacion)
        {
            var listado = _adConversacionDetalle.ObtenerMensajesNuevos(idUsuario, idConversacion);
            if(listado != null)
            {
                if (listado.Any())
                {
                    long idConversacionDetalleMayor = listado.Select(x => x.IdConversacionDetalle).Max();
                    long idUsuarioMatch = listado.Select(x => x.IdUsuarioEmisor).FirstOrDefault();
                    int resultado = MarcarMensajeComoLeido(idConversacionDetalleMayor, idUsuarioMatch);
                    if (resultado > 0)
                    {
                        return listado;
                    }
                }
            }
            return new List<ConversacionDetalleObtenerMensajeNuevoDto>();
            
        }

        public int MarcarMensajeComoLeido(long idConversacionDetalle, long idUsuarioEmisor)
        {
            return _adConversacionDetalle.MarcarMensajeComoLeido(idConversacionDetalle, idUsuarioEmisor);
        }

    }
}
