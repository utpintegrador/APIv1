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
    public class AdTipoBusqueda: Logger
    {
        //Busqueda
        public List<TipoBusquedaObtenerDto> Obtener(RequestTipoBusquedaObtenerDto filtro)
        {
            List<TipoBusquedaObtenerDto> resultado = new List<TipoBusquedaObtenerDto>();
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoBusquedaObtenerDto>(query, new
                    {
                        filtro.Buscar,
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

        //Obtener Busqueda
        public List<TipoBusquedaObtenerComboDto> ObtenerCombo()
        {
            List<TipoBusquedaObtenerComboDto> resultado = new List<TipoBusquedaObtenerComboDto>();
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoBusquedaObtenerComboDto>(query, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Obtener Busqueda por ID
        public TipoBusquedaObtenerPorIdDto ObtenerPorId(int id)
        {
            TipoBusquedaObtenerPorIdDto resultado = new TipoBusquedaObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<TipoBusquedaObtenerPorIdDto>(query, new
                    {
                        IdTipoBusqueda = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registrar Busqueda
        public int Registrar(RequestTipoBusquedaRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_Registrar";

                var p = new DynamicParameters();
                p.Add("IdTipoBusqueda", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdTipoBusqueda");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Busqueda
        public int Modificar(RequestTipoBusquedaModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdTipoBusqueda,
                        modelo.Descripcion
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar Busqueda
        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoBusqueda_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdTipoBusqueda = id,
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
