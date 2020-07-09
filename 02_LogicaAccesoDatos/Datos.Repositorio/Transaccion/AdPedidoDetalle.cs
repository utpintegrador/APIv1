using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Transaccion;
using Entidad.Request.Transaccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Transaccion
{
    public class AdPedidoDetalle: Logger
    {
        public List<PedidoDetalleObtenerPorIdPedidoDto> ObtenerPorIdPedido(long idPedido)
        {
            List<PedidoDetalleObtenerPorIdPedidoDto> resultado = new List<PedidoDetalleObtenerPorIdPedidoDto>();
            try
            {
                const string query = "Transaccion.usp_PedidoDetalle_ObtenerPorIdPedido";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoDetalleObtenerPorIdPedidoDto>(query, new
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

        public int Registrar(RequestPedidoDetalleRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_PedidoDetalle_Registrar";
                var p = new DynamicParameters();
                p.Add("IdPedidoDetalle", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdPedido", modelo.IdPedido);
                p.Add("IdProducto", modelo.IdProducto);
                p.Add("Cantidad", modelo.Cantidad);
                p.Add("PrecioUnitario", modelo.PrecioUnitario);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                    idNuevo = p.Get<long>("IdPedidoDetalle");
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestPedidoDetalleModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_PedidoDetalle_Modificar";
                var p = new DynamicParameters();
                p.Add("IdPedidoDetalle", modelo.IdPedidoDetalle);
                p.Add("IdPedido", modelo.IdPedido);
                p.Add("IdProducto", modelo.IdProducto);
                p.Add("Cantidad", modelo.Cantidad);
                p.Add("PrecioUnitario", modelo.PrecioUnitario);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Elminar(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_PedidoDetalle_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query,new {
                        IdPedidoDetalle = id
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
