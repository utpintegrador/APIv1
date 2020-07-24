using Datos.Repositorio.Grafico;
using Entidad.Dto.Grafico;
using Entidad.Request.Grafico;

namespace Negocio.Repositorio.Grafico
{
    public class LnGraficoWeb
    {
        private readonly AdGraficoWeb _adGraficoWeb = new AdGraficoWeb();

        public GraficoObtenerResumenDto ObtenerResumenWeb(RequestGraficoObtenerResumenDto prm)
        {
            return _adGraficoWeb.ObtenerResumenWeb(prm);
        }
    }
}
