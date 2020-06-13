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
    public class AdImagen: Logger
    {
        public List<ImagenObtenerDto> ObtenerPorIdAlbumImagen(long id)
        {
            List<ImagenObtenerDto> resultado = new List<ImagenObtenerDto>();
            try
            {
                const string query = "Perfil.usp_Imagen_ObtenerPorIdAlbumImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<ImagenObtenerDto>(query,new {
                        IdAlbumImagen = id
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public Imagen ObtenerPorId(long id)
        {
            Imagen resultado = new Imagen();
            try
            {
                const string query = "Perfil.usp_Imagen_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<Imagen>(query, new
                    {
                        IdImagen = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(ImagenRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_Imagen_Registrar";

                var p = new DynamicParameters();
                p.Add("IdImagen", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("Url", modelo.Url);
                p.Add("IdAlbumImagen", modelo.IdAlbumImagen);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdImagen");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(Imagen modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_Imagen_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdImagen,
                        modelo.Url,
                        modelo.IdAlbumImagen
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
                const string query = "Perfil.usp_Imagen_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdImagen = id,
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
