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
    }
}
