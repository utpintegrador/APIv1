using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Interaccion;
using System;
using System.Data;

namespace Datos.Repositorio.Interaccion
{
    public class AdPublicacionMeGusta: Logger
    {
        public int Registrar(PublicacionMeGustaRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_PublicacionMeGusta_Registrar";

                var p = new DynamicParameters();
                p.Add("IdPublicacionMeGusta", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdUsuarioQueEfectuaMeGusta", modelo.IdUsuarioQueEfectuaMeGusta);
                p.Add("IdPublicacion", modelo.IdPublicacion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdPublicacionMeGusta");

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
