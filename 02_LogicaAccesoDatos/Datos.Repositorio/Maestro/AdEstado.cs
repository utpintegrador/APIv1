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
    public class AdEstado : Logger
    {
        public List<EstadoObtenerDto> Obtener(RequestEstadoObtenerDto filtro)
        {
            //Obtener Estado
            List<EstadoObtenerDto> resultado = new List<EstadoObtenerDto>();
            try
            {
                const string query = "Maestro.usp_Estado_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<EstadoObtenerDto>(query,new {
                        filtro.Buscar,
                        filtro.IdTipoEstado,
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

        //Obtener Estado por ID
        public EstadoObtenerPorIdDto ObtenerPorId(int id)
        {
            EstadoObtenerPorIdDto resultado = new EstadoObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_Estado_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<EstadoObtenerPorIdDto>(query, new
                    {
                        IdEstado = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registrar Estado
        public int Registrar(RequestEstadoRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Estado_Registrar";

                var p = new DynamicParameters();
                p.Add("IdEstado", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);
                p.Add("IdTipoEstado", modelo.IdTipoEstado);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdEstado");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Estado
        public int Modificar(RequestEstadoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Estado_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdEstado,
                        modelo.Descripcion,
                        modelo.IdTipoEstado
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar Estado
        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Estado_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdEstado = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Listar Estado
        public List<EstadoObtenerComboDto> ObtenerCombo(int idTipoEstado)
        {
            List<EstadoObtenerComboDto> resultado = new List<EstadoObtenerComboDto>();
            try
            {
                const string query = "Maestro.usp_Estado_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<EstadoObtenerComboDto>(query,new {
                        IdTipoEstado = idTipoEstado
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<EstadoObtenerComboVendedorDto> ObtenerComboVendedor(int idEstadoActual)
        {
            List<EstadoObtenerComboVendedorDto> resultado = new List<EstadoObtenerComboVendedorDto>();
            try
            {
                const string query = "Maestro.usp_Estado_ObtenerComboVendedor";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<EstadoObtenerComboVendedorDto>(query, new
                    {
                        IdEstadoActual = idEstadoActual
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<EstadoObtenerComboCompradorDto> ObtenerComboComprador(int idEstadoActual)
        {
            List<EstadoObtenerComboCompradorDto> resultado = new List<EstadoObtenerComboCompradorDto>();
            try
            {
                const string query = "Maestro.usp_Estado_ObtenerComboComprador";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<EstadoObtenerComboCompradorDto>(query, new
                    {
                        IdEstadoActual = idEstadoActual
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
