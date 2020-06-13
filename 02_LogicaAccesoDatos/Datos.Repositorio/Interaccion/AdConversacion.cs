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
    public class AdConversacion: Logger
    {
        public List<ConversacionObtenerDto> ObtenerPorIdUsuario(long idUsuario)
        {
            List<ConversacionObtenerDto> resultado = new List<ConversacionObtenerDto>();
            try
            {
                const string query = "Interaccion.usp_Conversacion_ObtenerPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ConversacionObtenerDto>(query, new
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
