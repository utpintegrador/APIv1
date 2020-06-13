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
    public class AdContacto: Logger
    {
        public int Registrar(ContactoRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_Contacto_Registrar";

                var p = new DynamicParameters();
                p.Add("IdContacto", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdConversacion", modelo.IdUsuarioEmisor);
                p.Add("Contenido", modelo.IdUsuarioObjetivo);
                p.Add("IdUsuarioEmisor", modelo.IdEstadoValoracionContacto);
                p.Add("FechaRegistro", DateTime.Now);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdContacto");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<ContactoObtenerPendienteMatchDto> ObtenerPendienteMatch()
        {
            List<ContactoObtenerPendienteMatchDto> resultado = new List<ContactoObtenerPendienteMatchDto>();
            try
            {
                const string query = "Interaccion.usp_Contacto_ObtenerPendienteMatch";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ContactoObtenerPendienteMatchDto>(query, 
                        commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Eliminar(long idContacto)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_Contacto_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query,new {
                        IdContacto = idContacto
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
