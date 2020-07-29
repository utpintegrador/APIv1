using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Seguimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Seguimiento
{
    public class AdPedidoControlEstado : Logger
    {
        public List<PedidoControlEstadoObtenerPorIdPedidoDto> ObtenerPorIdPedido(long idPedido)
        {
            List<PedidoControlEstadoObtenerPorIdPedidoDto> resultado = new List<PedidoControlEstadoObtenerPorIdPedidoDto>();
            try
            {
                const string query = "Seguimiento.usp_PedidoControlEstado_ObtenerPorIdPedido";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoControlEstadoObtenerPorIdPedidoDto>(query, new
                    {
                        IdPedido = idPedido
                    }, commandType: CommandType.StoredProcedure).ToList();

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
