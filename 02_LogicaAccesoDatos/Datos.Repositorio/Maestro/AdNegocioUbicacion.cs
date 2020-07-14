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
    public class AdNegocioUbicacion: Logger
    {
        public List<NegocioUbicacionObtenerPorIdNegocioDto> ObtenerPorIdNegocio(RequestNegocioUbicacionObtenerPorIdNegocioDto filtro)
        {
            //Obtener Negocio Ubicacion
            List<NegocioUbicacionObtenerPorIdNegocioDto> resultado = new List<NegocioUbicacionObtenerPorIdNegocioDto>();
            try
            {
                const string query = "Maestro.usp_NegocioUbicacion_ObtenerPorIdNegocio";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<NegocioUbicacionObtenerPorIdNegocioDto>(query, new
                    {
                        filtro.Buscar,
                        filtro.IdNegocio,
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

        //Obtener Negocio Ubicacion por ID
        public NegocioUbicacionObtenerPorIdDto ObtenerPorId(long id)
        {
            NegocioUbicacionObtenerPorIdDto resultado = new NegocioUbicacionObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_NegocioUbicacion_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<NegocioUbicacionObtenerPorIdDto>(query, new
                    {
                        IdNegocioUbicacion = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registrar Negocio Ubicacion
        public int Registrar(RequestNegocioUbicacionRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_NegocioUbicacion_Registrar";

                var p = new DynamicParameters();
                p.Add("IdNegocioUbicacion", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdNegocio", modelo.IdNegocio);
                p.Add("Latitud", modelo.Latitud);
                p.Add("Longitud", modelo.Longitud);
                p.Add("Titulo", modelo.Titulo);
                p.Add("Descripcion", modelo.Descripcion);
                p.Add("Predeterminado", modelo.Predeterminado);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdNegocioUbicacion");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Negocio Ubicacion
        public int Modificar(RequestNegocioUbicacionModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_NegocioUbicacion_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdNegocioUbicacion,
                        modelo.Latitud,
                        modelo.Longitud,
                        modelo.Titulo,
                        modelo.Descripcion,
                        modelo.Predeterminado
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Eliminar Negocio Ubicacion
        public int Eliminar(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_NegocioUbicacion_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdNegocioUbicacion = id,
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
