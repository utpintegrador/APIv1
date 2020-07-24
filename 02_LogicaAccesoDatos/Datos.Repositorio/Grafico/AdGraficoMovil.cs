using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Grafico;
using Entidad.Request.Grafico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Grafico
{
    public class AdGraficoMovil: Logger
    {
        public List<GraficoObtenerResumenComprasDto> ObtenerResumenCompras(RequestGraficoObtenerResumenComprasDto prm)
        {
            List<GraficoObtenerResumenComprasDto> resultado = new List<GraficoObtenerResumenComprasDto>();
            try
            {
                const string query = "Grafico.usp_ObtenerResumenCompras";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<GraficoObtenerResumenComprasDto>(query, new
                    {
                        prm.IdUsuario,
                        prm.Desde,
                        prm.Hasta
                    }, commandType: CommandType.StoredProcedure).ToList(); ;

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<GraficoObtenerResumenVentasDto> ObtenerResumenVentas(RequestGraficoObtenerResumenVentasDto prm)
        {
            List<GraficoObtenerResumenVentasDto> resultado = new List<GraficoObtenerResumenVentasDto>();
            try
            {
                const string query = "Grafico.usp_ObtenerResumenVentas";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<GraficoObtenerResumenVentasDto>(query, new
                    {
                        prm.IdUsuario,
                        prm.Desde,
                        prm.Hasta
                    }, commandType: CommandType.StoredProcedure).ToList(); ;

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
