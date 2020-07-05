using Entidad.Entidad.Maestro;
using System;
using System.Data;
using System.Collections.Generic;
using Datos.Helper;
using Dapper;
using System.Linq;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;

namespace Datos.Repositorio.Maestro
{
    public class AdNegocio: Logger
    {
        public List<NegocioObtenerDto> Obtener(NegocioObtenerFiltroDto filtro)
        {
            List<NegocioObtenerDto> resultado = new List<NegocioObtenerDto>();
            try
            {
                const string query = "Maestro.usp_Negocio_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<NegocioObtenerDto>(query,new {
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdUsuario,
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

        public List<NegocioObtenerCercanosDto> ObtenerCercanos(NegocioObtenerCercanosFiltroDto filtro)
        {
            List<NegocioObtenerCercanosDto> resultado = new List<NegocioObtenerCercanosDto>();
            try
            {
                const string query = "Maestro.usp_Negocio_ObtenerCercanos";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<NegocioObtenerCercanosDto>(query, new
                    {
                        filtro.Buscar,
                        filtro.CantidadKilometros,
                        filtro.IdUsuario,
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

        public NegocioObtenerPorIdDto ObtenerPorId(long id)
        {
            NegocioObtenerPorIdDto resultado = new NegocioObtenerPorIdDto();
            try
            {
                const string query = "Maestro.usp_Negocio_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<NegocioObtenerPorIdDto>(query, new
                    {
                        IdNegocio = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(NegocioRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Negocio_Registrar";

                var p = new DynamicParameters();
                p.Add("IdNegocio", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("DocumentoIdentificacion", modelo.DocumentoIdentificacion);
                p.Add("Nombre", modelo.Nombre);
                p.Add("Resenia", modelo.Resenia);
                p.Add("IdTipoDocumentoIdentificacion", modelo.IdTipoDocumentoIdentificacion);
                p.Add("IdUsuario", modelo.IdUsuario);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdNegocio");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(NegocioModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Negocio_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdNegocio,
                        modelo.DocumentoIdentificacion,
                        modelo.Nombre,
                        modelo.Resenia,
                        modelo.IdTipoDocumentoIdentificacion,
                        modelo.IdUsuario,
                        modelo.IdEstado
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Eliminar(int id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Negocio_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdNegocio = id,
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
