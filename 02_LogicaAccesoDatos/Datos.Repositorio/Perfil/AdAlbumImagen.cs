using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Perfil;
using Entidad.Entidad.Perfil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Perfil
{
    public class AdAlbumImagen: Logger
    {
        public List<AlbumImagenObtenerDto> ObtenerPorIdPerfil(long id)
        {
            List<AlbumImagenObtenerDto> resultado = new List<AlbumImagenObtenerDto>();
            try
            {
                const string query = "Perfil.usp_AlbumImagen_ObtenerPorIdPerfil";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<AlbumImagenObtenerDto>(query,new {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public AlbumImagen ObtenerPorId(long id)
        {
            AlbumImagen resultado = new AlbumImagen();
            try
            {
                const string query = "Perfil.usp_AlbumImagen_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<AlbumImagen>(query, new
                    {
                        IdAlbumImagen = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(AlbumImagenRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_AlbumImagen_Registrar";

                var p = new DynamicParameters();
                p.Add("IdAlbumImagen", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("Titulo", modelo.Titulo);
                p.Add("Descripcion", modelo.Descripcion);
                p.Add("UrlImagenAlbum", modelo.UrlImagenAlbum);
                p.Add("IdPerfil", modelo.IdPerfil);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdAlbumImagen");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(AlbumImagen modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_AlbumImagen_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdAlbumImagen,
                        modelo.Titulo,
                        modelo.Descripcion,
                        modelo.UrlImagenAlbum
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Eliminar(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_AlbumImagen_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdAlbumImagen = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int EliminarImagen(long id)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_AlbumImagen_EliminarImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdAlbumImagen = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarImagen(AlbumImagen modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_AlbumImagen_ModificarImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdAlbumImagen,
                        modelo.UrlImagenAlbum
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
