using System;
using System.Data;
using System.Collections.Generic;
using Datos.Helper;
using Dapper;
using System.Linq;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;

namespace Datos.Repositorio.Maestro
{
    public class AdMoneda: Logger
    {
        //Obtener Moneda
        public List<MonedaObtenerDto> Obtener(RequestMonedaObtenerDto filtro)
        {
            List<MonedaObtenerDto> resultado = new List<MonedaObtenerDto>();
            try
            {
                const string query = "Maestro.usp_Moneda_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<MonedaObtenerDto>(query,new {
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

        //Obtener Moneda por ID
        public MonedaObtenerPorIdDto ObtenerPorId(int id)
        {
            MonedaObtenerPorIdDto resultado = new MonedaObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_Moneda_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<MonedaObtenerPorIdDto>(query, new
                    {
                        IdMoneda = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registar Moneda
        public int Registrar(RequestMonedaRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Moneda_Registrar";

                var p = new DynamicParameters();
                p.Add("IdMoneda", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);
                p.Add("Simbolo", modelo.Simbolo);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdMoneda");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Moneda
        public int Modificar(RequestMonedaModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Moneda_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdMoneda,
                        modelo.Descripcion,
                        modelo.Simbolo
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar Moneda
        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {                
                const string query = "Maestro.usp_Moneda_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdMoneda = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Lstar Moneda
        public List<MonedaObtenerComboDto> ObtenerCombo()
        {
            List<MonedaObtenerComboDto> resultado = new List<MonedaObtenerComboDto>();
            try
            {
                const string query = "Maestro.usp_Moneda_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<MonedaObtenerComboDto>(query, commandType: CommandType.StoredProcedure).ToList();

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
