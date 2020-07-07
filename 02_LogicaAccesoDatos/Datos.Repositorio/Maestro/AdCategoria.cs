using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using Entidad.Request.Maestro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Maestro
{
    public class AdCategoria : Logger
    {
        public List<CategoriaObtenerDto> Obtener(RequestCategoriaObtenerDto filtro)
        {
            List<CategoriaObtenerDto> resultado = new List<CategoriaObtenerDto>();
            try
            {
                const string query = "Maestro.usp_Categoria_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<CategoriaObtenerDto>(query,new {
                        filtro.Buscar,
                        filtro.IdEstado,
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

        public Categoria ObtenerPorId(int id)
        {
            Categoria resultado = new Categoria();
            try
            {
                const string query = "Maestro.usp_Categoria_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<Categoria>(query, new
                    {
                        IdCategoria = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(RequestCategoriaRegistrarDto modelo, ref int idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Categoria_Registrar";

                var p = new DynamicParameters();
                p.Add("IdCategoria", 0, DbType.Int32, ParameterDirection.Output);
                p.Add("Descripcion", modelo.Descripcion);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<int>("IdCategoria");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestCategoriaModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Categoria_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdCategoria,
                        modelo.Descripcion,
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
                const string query = "Maestro.usp_Categoria_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdCategoria = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarUrlImagenPorIdCategoria(long idCategoria, string url)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Categoria_ModificarUrlImagenPorIdCategoria";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdCategoria = idCategoria,
                        UrlImagen = url
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public CategoriaObtenerUrlImagenDto ObtenerUrlImagenPorId(long id)
        {
            CategoriaObtenerUrlImagenDto resultado = new CategoriaObtenerUrlImagenDto();
            try
            {
                const string query = "Maestro.usp_Categoria_ObtenerUrlImagenPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<CategoriaObtenerUrlImagenDto>(query, new
                    {
                        IdCategoria = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int EliminarUrlImagen(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Maestro.usp_Categoria_EliminarUrlImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdCategoria = id
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
