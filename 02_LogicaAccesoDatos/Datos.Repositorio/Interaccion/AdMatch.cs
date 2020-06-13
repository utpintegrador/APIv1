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
    public class AdMatch: Logger
    {
        public int Registrar(MatchRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_Match_Registrar";

                var p = new DynamicParameters();
                p.Add("IdMatch", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdUsuarioEmisor", modelo.IdUsuarioEmisor);
                p.Add("IdEstadoValoracionUsuarioEmisor", modelo.IdEstadoValoracionUsuarioEmisor);
                p.Add("IdUsuarioMatch", modelo.IdUsuarioMatch);
                p.Add("IdEstadoValoracionUsuarioMatch", modelo.IdEstadoValoracionUsuarioMatch);
                p.Add("FechaRegistro", DateTime.Now);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdMatch");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<MatchObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            List<MatchObtenerPorIdUsuarioDto> resultado = new List<MatchObtenerPorIdUsuarioDto>();
            try
            {
                const string query = "Interaccion.usp_Match_ObtenerPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<MatchObtenerPorIdUsuarioDto>(query, new
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

    }
}
