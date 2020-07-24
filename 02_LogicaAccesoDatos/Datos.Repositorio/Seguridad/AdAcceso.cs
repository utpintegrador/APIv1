using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Datos.Repositorio.Seguridad
{
    public class AdAcceso: Logger
    {
        public List<AccesoObtenerDto> Obtener(RequestAccesoObtenerDto filtro)
        {
            List<AccesoObtenerDto> resultado = new List<AccesoObtenerDto>();
            try
            {
                const string query = "Seguridad.usp_Acceso_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<AccesoObtenerDto>(query, new
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

        public AccesoObtenerPorIdDto ObtenerPorId(int id)
        {
            AccesoObtenerPorIdDto resultado = new AccesoObtenerPorIdDto();
            try
            {
                const string query = "Seguridad.usp_Acceso_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<AccesoObtenerPorIdDto>(query, new
                    {
                        IdAcceso = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(RequestAccesoRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Acceso_Registrar";

                var p = new DynamicParameters();
                p.Add("IdAcceso", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Titulo", modelo.Titulo);
                p.Add("UrlAcceso", modelo.UrlAcceso);
                p.Add("Icono", modelo.Icono);
                p.Add("IdAccesoPadre", modelo.IdAccesoPadre);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdAcceso");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestAccesoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Acceso_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdAcceso,
                        modelo.Titulo,
                        modelo.UrlAcceso,
                        modelo.Icono,
                        modelo.IdAccesoPadre
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
                const string query = "Seguridad.usp_Acceso_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdAcceso = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<AccesoObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            List<AccesoObtenerPorIdUsuarioDto> resultado = new List<AccesoObtenerPorIdUsuarioDto>();
            try
            {
                const string query = "Seguridad.usp_Acceso_ObtenerPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<AccesoObtenerPorIdUsuarioDto>(query, new
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
