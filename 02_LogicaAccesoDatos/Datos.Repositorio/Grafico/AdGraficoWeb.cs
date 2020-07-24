using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Grafico;
using Entidad.Request.Grafico;
using System;
using System.Data;

namespace Datos.Repositorio.Grafico
{
    public class AdGraficoWeb: Logger
    {
        public GraficoObtenerResumenDto ObtenerResumenWeb(RequestGraficoObtenerResumenDto prm)
        {
            GraficoObtenerResumenDto resultado = new GraficoObtenerResumenDto();
            try
            {
                const string query = "Grafico.usp_ObtenerResumenWeb";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<GraficoObtenerResumenDto>(query, new
                    {
                        prm.IdUsuario,
                        prm.Anio,
                        prm.Mes
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }
    }
}
