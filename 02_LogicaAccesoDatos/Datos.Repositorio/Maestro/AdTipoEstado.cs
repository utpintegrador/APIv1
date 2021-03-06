﻿using System;
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
    public class AdTipoEstado : Logger
    {
        //Tipo Estado
        public List<TipoEstadoObtenerDto> Obtener(RequestTipoEstadoObtenerDto filtro)
        {
            List<TipoEstadoObtenerDto> resultado = new List<TipoEstadoObtenerDto>();
            try
            {
                const string query = "Maestro.usp_TipoEstado_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoEstadoObtenerDto>(query,new {
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

        //Obtener Tipo Estado por ID
        public TipoEstadoObtenerPorIdDto ObtenerPorId(int id)
        {
            TipoEstadoObtenerPorIdDto resultado = new TipoEstadoObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_TipoEstado_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<TipoEstadoObtenerPorIdDto>(query, new
                    {
                        IdTipoEstado = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registrar Tipo Estado
        public int Registrar(RequestTipoEstadoRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoEstado_Registrar";

                var p = new DynamicParameters();
                p.Add("IdTipoEstado", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdTipoEstado");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Tipo Estado
        public int Modificar(RequestTipoEstadoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoEstado_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new {
                        modelo.IdTipoEstado,
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

        //Eliminar Tipo Estado
        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoEstado_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdTipoEstado = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Listar Tipo Estado
        public List<TipoEstadoObtenerComboDto> ObtenerCombo()
        {
            List<TipoEstadoObtenerComboDto> resultado = new List<TipoEstadoObtenerComboDto>();
            try
            {
                const string query = "Maestro.usp_TipoEstado_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoEstadoObtenerComboDto>(query, commandType: CommandType.StoredProcedure).ToList();

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
