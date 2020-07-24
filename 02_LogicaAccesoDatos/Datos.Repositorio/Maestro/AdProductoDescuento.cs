using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Maestro
{
    public class AdProductoDescuento: Logger
    {
        public List<ProductoDescuentoObtenerPorIdProductoDto> ObtenerPorIdProducto(RequestProductoDescuentoObtenerPorIdProductoDto filtro)
        {
            List<ProductoDescuentoObtenerPorIdProductoDto> resultado = new List<ProductoDescuentoObtenerPorIdProductoDto>();
            try
            {
                const string query = "Maestro.usp_ProductoDescuento_ObtenerPorIdProducto";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ProductoDescuentoObtenerPorIdProductoDto>(query, new
                    {
                        filtro.IdProducto,
                        filtro.NumeroPagina,
                        filtro.CantidadRegistros,
                        filtro.ColumnaOrden,
                        filtro.DireccionOrden
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Obtener ProductoDescuento por ID
        public ProductoDescuentoObtenerPorIdDto ObtenerPorId(long id)
        {
            ProductoDescuentoObtenerPorIdDto resultado = new ProductoDescuentoObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_ProductoDescuento_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<ProductoDescuentoObtenerPorIdDto>(query, new
                    {
                        IdProductoDescuento = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registar ProductoDescuento
        public int Registrar(RequestProductoDescuentoRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_ProductoDescuento_Registrar";

                var p = new DynamicParameters();
                p.Add("IdProductoDescuento", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdProducto", modelo.IdProducto);
                p.Add("FechaInicio", modelo.FechaInicioDate);
                p.Add("FechaFin", modelo.FechaFinDate);
                p.Add("IdTipoDescuento", modelo.IdTipoDescuento);
                p.Add("Valor", modelo.Valor);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdProductoDescuento");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar ProductoDescuento
        public int Modificar(RequestProductoDescuentoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_ProductoDescuento_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdProductoDescuento,
                        FechaInicio = modelo.FechaInicioDate,
                        FechaFin = modelo.FechaFinDate,
                        modelo.IdTipoDescuento,
                        modelo.Valor,
                        modelo.IdEstado
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar ProductoDescuento
        public int Eliminar(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_ProductoDescuento_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdProductoDescuento = id,
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
