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
    public class AdConversacionDetalle: Logger
    {
        public List<ConversacionDetalleObtenerDto> ObtenerPorIdConversacion(long idConversacion)
        {
            List<ConversacionDetalleObtenerDto> resultado = new List<ConversacionDetalleObtenerDto>();
            try
            {
                const string query = "Interaccion.usp_ConversacionDetalle_ObtenerPorIdConversacion";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ConversacionDetalleObtenerDto>(query, new
                    {
                        IdConversacion = idConversacion
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(ConversacionDetalleRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_ConversacionDetalle_Registrar";

                var p = new DynamicParameters();
                p.Add("IdConversacionDetalle", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdConversacion", modelo.IdConversacion);
                p.Add("Contenido", modelo.Contenido);
                p.Add("IdUsuarioEmisor", modelo.IdUsuarioEmisor);
                p.Add("FechaRegistro", DateTime.Now);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdConversacionDetalle");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<ConversacionDetalleObtenerMensajeNuevoDto> ObtenerMensajesNuevos(long idUsuario, long idConversacion)
        {
            List<ConversacionDetalleObtenerMensajeNuevoDto> resultado = new List<ConversacionDetalleObtenerMensajeNuevoDto>();
            try
            {
                const string query = "Interaccion.usp_ConversacionDetalle_ObtenerMensajesNuevos";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ConversacionDetalleObtenerMensajeNuevoDto>(query, new
                    {
                        IdUsuario = idUsuario,
                        IdConversacion = idConversacion
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int MarcarMensajeComoLeido(long idConversacionDetalle, long idUsuarioEmisor)
        {
            int resultado = 0;
            try
            {
                const string query = "Interaccion.usp_ConversacionDetalle_MarcarMensajeComoLeido";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query,new {
                        IdConversacionDetalle = idConversacionDetalle,
                        IdUsuarioEmisor = idUsuarioEmisor
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
