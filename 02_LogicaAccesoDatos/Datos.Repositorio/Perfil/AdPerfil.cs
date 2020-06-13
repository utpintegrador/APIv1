using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Perfil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Perfil
{
    public class AdPerfil: Logger
    {
        public PerfilObtenerDto ObtenerPorIdUsuario(long idUsuario)
        {
            PerfilObtenerDto resultado = new PerfilObtenerDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PerfilObtenerDto>(query, new
                    {
                        IdUsuario = idUsuario
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public Entidad.Entidad.Perfil.Perfil ObtenerPorId(long id)
        {
            Entidad.Entidad.Perfil.Perfil resultado = new Entidad.Entidad.Perfil.Perfil();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<Entidad.Entidad.Perfil.Perfil>(query, new
                    {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(PerfilRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                DateTime? fechaNacimiento = Entidad.Utilitario.Util.ObtenerFechaDesdeString(modelo.FechaNacimiento);
                const string query = "Perfil.usp_Perfil_Registrar";

                var p = new DynamicParameters();
                p.Add("IdPerfil", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("FechaNacimiento", fechaNacimiento);
                p.Add("Biografia", modelo.Biografia);
                p.Add("Direccion", modelo.Direccion);
                p.Add("Telefono", modelo.Telefono);
                p.Add("IdEstadoOcupacional", modelo.IdEstadoOcupacional);
                p.Add("NombreInstitucionLaboral", modelo.NombreInstitucionLaboral);
                p.Add("DescripcionCargoLaboral", modelo.DescripcionCargoLaboral);
                p.Add("IdGradoAcademico", modelo.IdGradoAcademico);
                p.Add("NombreCortoInstitucionAcademica", modelo.NombreCortoInstitucionAcademica);
                p.Add("IdCarrera", modelo.IdCarrera);
                p.Add("IdPaisResidencia", modelo.IdPaisResidencia);
                p.Add("IdUsuario", modelo.IdUsuario);
                p.Add("IdGenero", modelo.IdGenero);
                p.Add("IdEstadoSituacionSentimental", modelo.IdEstadoSituacionSentimental);
                p.Add("IdInteresGenero", modelo.IdInteresGenero);
                p.Add("IdInteresSentimental", modelo.IdInteresSentimental);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);

                    idNuevo = p.Get<long>("IdPerfil");

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(PerfilModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                DateTime? fechaNacimiento = Entidad.Utilitario.Util.ObtenerFechaDesdeString(modelo.FechaNacimiento);
                const string query = "Perfil.usp_Perfil_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPerfil,
                        FechaNacimiento = fechaNacimiento,
                        modelo.Biografia,
                        modelo.Direccion,
                        modelo.Telefono,
                        modelo.IdEstadoOcupacional,
                        modelo.NombreInstitucionLaboral,
                        modelo.DescripcionCargoLaboral,
                        modelo.IdGradoAcademico,
                        modelo.NombreCortoInstitucionAcademica,
                        modelo.IdCarrera,
                        modelo.IdPaisResidencia,
                        modelo.IdGenero,
                        modelo.IdEstadoSituacionSentimental,
                        modelo.IdInteresGenero,
                        modelo.IdInteresSentimental,
                        modelo.IdUsuario
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PerfilObtenerDatoAcademicoDto ObtenerDatoAcademicoPorId(long id)
        {
            PerfilObtenerDatoAcademicoDto resultado = new PerfilObtenerDatoAcademicoDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerDatoAcademicoPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PerfilObtenerDatoAcademicoDto>(query, new
                    {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PerfilObtenerDatoLaboralDto ObtenerDatoLaboralPorId(long id)
        {
            PerfilObtenerDatoLaboralDto resultado = new PerfilObtenerDatoLaboralDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerDatoLaboralPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PerfilObtenerDatoLaboralDto>(query, new
                    {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PerfilObtenerInformacionDto ObtenerInformacionPorId(long id)
        {
            PerfilObtenerInformacionDto resultado = new PerfilObtenerInformacionDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerInformacionPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PerfilObtenerInformacionDto>(query, new
                    {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PerfilObtenerInteresesDto ObtenerInteresesPorId(long id)
        {
            PerfilObtenerInteresesDto resultado = new PerfilObtenerInteresesDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerInteresesPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PerfilObtenerInteresesDto>(query, new
                    {
                        IdPerfil = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }
        
        public int ModificarDatoAcademico(PerfilModificarDatoAcademicoDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_Perfil_ModificarDatoAcademico";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPerfil,
                        modelo.IdGradoAcademico,
                        modelo.IdCarrera,
                        modelo.NombreCortoInstitucionAcademica
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarDatoLaboral(PerfilModificarDatoLaboralDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_Perfil_ModificarDatoLaboral";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPerfil,
                        modelo.IdEstadoOcupacional,
                        modelo.NombreInstitucionLaboral,
                        modelo.DescripcionCargoLaboral
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarInformacion(PerfilModificarInformacionDto modelo)
        {
            int resultado = 0;
            try
            {
                DateTime? fechaNacimiento = Entidad.Utilitario.Util.ObtenerFechaDesdeString(modelo.FechaNacimiento);
                const string query = "Perfil.usp_Perfil_ModificarInformacion";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPerfil,
                        modelo.Biografia,
                        modelo.IdGenero,
                        FechaNacimiento = fechaNacimiento,
                        modelo.Telefono,
                        modelo.Direccion,
                        modelo.IdPaisResidencia
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarIntereses(PerfilModificarInteresesDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Perfil.usp_Perfil_ModificarIntereses";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPerfil,
                        modelo.IdEstadoSituacionSentimental,
                        modelo.IdInteresGenero,
                        modelo.IdInteresSentimental
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //public int ModificarUrlImagen(PerfilModificarUrlImagenDto modelo)
        //{
        //    int resultado = 0;
        //    try
        //    {
        //        const string query = "Perfil.usp_Perfil_ModificarUrlImagen";

        //        using (var cn = HelperClass.ObtenerConeccion())
        //        {
        //            if (cn.State == ConnectionState.Closed)
        //            {
        //                cn.Open();
        //            }

        //            resultado = cn.Execute(query, new
        //            {
        //                modelo.IdPerfil,
        //                modelo.UrlImagen
        //            }, commandType: CommandType.StoredProcedure);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
        //    }
        //    return resultado;
        //}

        

        public List<PerfilObtenerPasarelaDto> ObtenerPasarela(long idUsuario)
        {
            List<PerfilObtenerPasarelaDto> resultado = new List<PerfilObtenerPasarelaDto>();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerPasarela";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PerfilObtenerPasarelaDto>(query, new
                    {
                        IdUsuario = idUsuario
                    }, commandType: CommandType.StoredProcedure).ToList(); ;

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PerfilObtenerInformacionCompletaDto ObtenerInformacionCompleta(long idUsuario)
        {
            PerfilObtenerInformacionCompletaDto resultado = new PerfilObtenerInformacionCompletaDto();
            try
            {
                const string query = "Perfil.usp_Perfil_ObtenerInformacionCompleta";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QueryFirstOrDefault<PerfilObtenerInformacionCompletaDto>(query, new
                    {
                        IdUsuario = idUsuario
                    }, commandType: CommandType.StoredProcedure); ;

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        //public int ModificarUrlImagenPorIdUsuario(long idUsuario, string url)
        //{
        //    int resultado = 0;
        //    try
        //    {
        //        const string query = "Perfil.usp_Perfil_ModificarUrlImagenPorIdUsuario";

        //        using (var cn = HelperClass.ObtenerConeccion())
        //        {
        //            if (cn.State == ConnectionState.Closed)
        //            {
        //                cn.Open();
        //            }

        //            resultado = cn.Execute(query, new
        //            {
        //                IdUsuario = idUsuario,
        //                UrlImagen = url
        //            }, commandType: CommandType.StoredProcedure);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
        //    }
        //    return resultado;
        //}



    }
}
