using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Interaccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Interaccion
{
    public class AdPublicacion: Logger
    {
        public List<PublicacionObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            List<PublicacionObtenerPorIdUsuarioDto> resultado = new List<PublicacionObtenerPorIdUsuarioDto>();
            try
            {
                const string query = "Interaccion.usp_Publicacion_ObtenerPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PublicacionObtenerPorIdUsuarioDto>(query, new
                    {
                        IdUsuario = idUsuario
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PublicacionObtenerPorIdUsuarioDto ObtenerPorId(long id)
        {
            PublicacionObtenerPorIdUsuarioDto resultado = null;
            try
            {
                const string query = "Interaccion.usp_Publicacion_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PublicacionObtenerPorIdUsuarioDto>(query, new
                    {
                        IdPublicacion = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(PublicacionRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_Publicacion_Registrar";

                var p = new DynamicParameters();
                p.Add("IdPublicacion", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdUsuario", modelo.IdUsuario);
                p.Add("Texto", modelo.Texto);
                p.Add("IdPrivacidad", modelo.IdPrivacidad);
                p.Add("Origen", modelo.Origen);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdPublicacion");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Eliminar(long idPublicacion)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_Publicacion_Eliminar";

                var p = new DynamicParameters();
                p.Add("Respuesta", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("IdPublicacion", idPublicacion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    int ejecucion = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                    resultado = p.Get<int>("Respuesta");

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
