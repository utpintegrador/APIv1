using Datos.Repositorio.Interaccion;
using Entidad.Dto.Interaccion;

namespace Negocio.Repositorio.Interaccion
{
    public class LnPublicacionMeGusta
    {
        private readonly AdPublicacionMeGusta _adPublicacionMeGusta = new AdPublicacionMeGusta();
        public int Registrar(PublicacionMeGustaRegistrarDto modelo, ref long idNuevo)
        {
            return _adPublicacionMeGusta.Registrar(modelo, ref idNuevo);
        }
    }
}
