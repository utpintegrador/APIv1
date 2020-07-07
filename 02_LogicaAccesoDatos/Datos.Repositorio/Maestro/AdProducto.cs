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
    public class AdProducto: Logger
    {
        public List<ProductoObtenerPorIdNegocioDto> ObtenerPorIdNegocio(RequestProductoObtenerPorIdNegocioDto filtro)
        {
            List<ProductoObtenerPorIdNegocioDto> resultado = new List<ProductoObtenerPorIdNegocioDto>();
            try
            {
                const string query = "Maestro.usp_Producto_ObtenerPorIdNegocio";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ProductoObtenerPorIdNegocioDto>(query, new
                    {
                        filtro.IdNegocio,
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdMoneda,
                        filtro.IdCategoria,
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

        public ProductoObtenerPorIdDto ObtenerPorId(long id)
        {
            ProductoObtenerPorIdDto resultado = new ProductoObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_Producto_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<ProductoObtenerPorIdDto>(query, new
                    {
                        IdProducto = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(RequestProductoRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Producto_Registrar";

                var p = new DynamicParameters();
                p.Add("IdProducto", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);
                p.Add("DescripcionExtendida", modelo.DescripcionExtendida);
                p.Add("Precio", modelo.Precio);
                p.Add("IdMoneda", modelo.IdMoneda);
                p.Add("IdCategoria", modelo.IdCategoria);
                p.Add("IdNegocio", modelo.IdNegocio);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdProducto");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestProductoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Producto_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdProducto,
                        modelo.Descripcion,
                        modelo.DescripcionExtendida,
                        modelo.Precio,
                        modelo.IdMoneda,
                        modelo.IdCategoria,
                        modelo.IdNegocio
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Eliminar(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Producto_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdProducto = id,
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
