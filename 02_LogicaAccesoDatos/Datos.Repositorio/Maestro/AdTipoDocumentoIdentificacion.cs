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
    public class AdTipoDocumentoIdentificacion: Logger
    {
        //Documento de Identificacion
        public List<TipoDocumentoIdentificacionObtenerDto> Obtener(RequestTipoDocumentoIdentificacionObtenerDto filtro)
        {
            List<TipoDocumentoIdentificacionObtenerDto> resultado = new List<TipoDocumentoIdentificacionObtenerDto>();
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoDocumentoIdentificacionObtenerDto>(query, new
                    {
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

        //Obtener Documento de Identificacion
        public List<TipoDocumentoIdentificacionObtenerComboDto> ObtenerCombo()
        {
            List<TipoDocumentoIdentificacionObtenerComboDto> resultado = new List<TipoDocumentoIdentificacionObtenerComboDto>();
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<TipoDocumentoIdentificacionObtenerComboDto>(query, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Obtener Documento de Identificacion por ID
        public TipoDocumentoIdentificacionObtenerPorIdDto ObtenerPorId(int id)
        {
            TipoDocumentoIdentificacionObtenerPorIdDto resultado = new TipoDocumentoIdentificacionObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<TipoDocumentoIdentificacionObtenerPorIdDto>(query, new
                    {
                        IdTipoDocumentoIdentificacion = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Registrar Documento de Identificacion
        public int Registrar(RequestTipoDocumentoIdentificacionRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_Registrar";

                var p = new DynamicParameters();
                p.Add("IdTipoDocumentoIdentificacion", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdTipoDocumentoIdentificacion");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //Modificar Documento de Identificacion
        public int Modificar(RequestTipoDocumentoIdentificacionModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdTipoDocumentoIdentificacion,
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

        //Eliminar Documento de Identificacion
        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_TipoDocumentoIdentificacion_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdTipoDocumentoIdentificacion = id,
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
