using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Correo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Correo
{
    public class AdRecuperacionContrasenia: Logger
    {
        public RecuperacionContraseniaObtenerRegistradoDto Registrar(string correoElectronico)
        {
            /*
                NumeroRespuesta	DescripcionRespuesta	                    CorreoElectronico	        Codigo	IdUsuario
                3	            Ya existe un codigo pendiente de utilizar	frankronald1@hotmail.com		    0
             */
            RecuperacionContraseniaObtenerRegistradoDto resultado = new RecuperacionContraseniaObtenerRegistradoDto();
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_Registrar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QueryFirstOrDefault<RecuperacionContraseniaObtenerRegistradoDto>(query,new {
                        CorreoElectronico = correoElectronico
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<RecuperacionContraseniaObtenerDto> ObtenerPendientesProceso()
        {
            List<RecuperacionContraseniaObtenerDto> resultado = new List<RecuperacionContraseniaObtenerDto>();
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_ObtenerPendientesEnvio";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<RecuperacionContraseniaObtenerDto>(query, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarEstadoEnviado(long idRecuperacionContrasenia)
        {
            int resultado = 0;
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_ModificarEstadoEnviado";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdRecuperacionContrasenia = idRecuperacionContrasenia
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int EliminarProcesados()
        {
            int resultado = 0;
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_EliminarProcesados";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public RecuperacionContraseniaObtenerPorCodigoDto ObtenerUsuarioPorCodigo(string codigo)
        {
            RecuperacionContraseniaObtenerPorCodigoDto resultado = new RecuperacionContraseniaObtenerPorCodigoDto();
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_ObtenerUsuarioPorCodigo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QueryFirstOrDefault<RecuperacionContraseniaObtenerPorCodigoDto>(query, new
                    {
                        Codigo = codigo
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarContraseniaMedianteCodigo(RecuperacionContraseniaModificarContraseniaDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Correo.usp_RecuperacionContrasenia_ModificarContraseniaMedianteCodigo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdUsuario,
                        modelo.Contrasenia,
                        modelo.Codigo
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
