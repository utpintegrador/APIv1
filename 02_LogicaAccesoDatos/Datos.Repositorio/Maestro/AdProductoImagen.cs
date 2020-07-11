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
    public class AdProductoImagen: Logger
    {
        public List<ProductoImagenObtenerPorIdProductoDto> ObtenerPorIdProducto(RequestProductoImagenObtenerPorIdProductoDto filtro)
        {
            //Producto Imagen
            List<ProductoImagenObtenerPorIdProductoDto> resultado = new List<ProductoImagenObtenerPorIdProductoDto>();
            try
            {
                const string query = "Maestro.usp_ProductoImagen_ObtenerPorIdProducto";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ProductoImagenObtenerPorIdProductoDto>(query, new
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

        //Registrar Producto Imagen
        public int Registrar(long idProducto, string url, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_ProductoImagen_Registrar";

                var p = new DynamicParameters();
                p.Add("IdProductoImagen", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdProducto", idProducto);
                p.Add("UrlImagen", url);
                p.Add("Predeterminado", false);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdProductoImagen");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Obtener Producto Imagen
        public ProductoImagenObtenerUrlImagenDto ObtenerUrlImagenPorId(long id)
        {
            ProductoImagenObtenerUrlImagenDto resultado = new ProductoImagenObtenerUrlImagenDto();
            try
            {
                const string query = "Maestro.usp_ProductoImagen_ObtenerUrlImagenPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<ProductoImagenObtenerUrlImagenDto>(query, new
                    {
                        IdProductoImagen = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar Imagen
        public int EliminarUrlImagen(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_ProductoImagen_EliminarUrlImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdProductoImagen = id
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
