﻿using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Seguridad
{
    public class AdUsuario: Logger
    {
        public UsuarioLoginDto ObtenerPorLogin(RequestUsuarioCredencialesDto modelo)
        {
            UsuarioLoginDto resultado = new UsuarioLoginDto();
            try
            {
                const string query = "Seguridad.usp_Usuario_ObtenerPorLogeo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    /*los alias en el parametro son opcionales si la propiedad del modelo tiene la misma denominacion*/
                    resultado = cn.QuerySingleOrDefault<UsuarioLoginDto>(query, new
                    {
                        CorreoElectronico = modelo.CorreoElectronico,
                        modelo.Contrasenia
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<UsuarioObtenerDto> Obtener(RequestUsuarioObtenerDto filtro)
        {
            List<UsuarioObtenerDto> resultado = new List<UsuarioObtenerDto>();
            try
            {
                const string query = "Seguridad.usp_Usuario_Obtener";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<UsuarioObtenerDto>(query,new {
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdTipoUsuario,
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

        public UsuarioObtenerPorIdDto ObtenerPorId(long id)
        {
            UsuarioObtenerPorIdDto resultado = new UsuarioObtenerPorIdDto();
            try
            {
                const string query = "Seguridad.usp_Usuario_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<UsuarioObtenerPorIdDto>(query, new
                    {
                        IdUsuario = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(RequestUsuarioRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Usuario_Registrar";
                var p = new DynamicParameters();
                p.Add("IdUsuario", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("CorreoElectronico", modelo.CorreoElectronico);
                p.Add("Contrasenia", modelo.Contrasenia);
                p.Add("Nombre", modelo.Nombre);
                p.Add("Apellido", modelo.Apellido);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                    idNuevo = p.Get<long>("IdUsuario");
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestUsuarioModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Usuario_Modificar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdUsuario,
                        modelo.CorreoElectronico,
                        modelo.Nombre,
                        modelo.Apellido
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarModoAdmin(RequestUsuarioModificarModoAdminDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Usuario_ModificarModoAdmin";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdUsuario,
                        modelo.CorreoElectronico,
                        modelo.Nombre,
                        modelo.Apellido,
                        modelo.IdEstado,
                        modelo.IdTipoUsuario
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
                const string query = "Seguridad.usp_Usuario_Eliminar";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdUsuario = id,
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarContrasenia(RequestUsuarioCambioContraseniaDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Usuario_ModificarContrasenia";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdUsuario,
                        modelo.Contrasenia
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarUrlImagenPorIdUsuario(long idUsuario, string url)
        {
            int resultado = 0;
            try
            {
                const string query = "Seguridad.usp_Usuario_ModificarUrlImagenPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdUsuario = idUsuario,
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

        public UsuarioObtenerUrlImagenDto ObtenerUrlImagenPorId(long id)
        {
            UsuarioObtenerUrlImagenDto resultado = new UsuarioObtenerUrlImagenDto();
            try
            {
                const string query = "Seguridad.usp_Usuario_ObtenerUrlImagenPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<UsuarioObtenerUrlImagenDto>(query, new
                    {
                        IdUsuario = id
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
                const string query = "Seguridad.usp_Usuario_EliminarUrlImagen";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Execute(query, new
                    {
                        IdUsuario = id
                    }, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<UsuarioObtenerComboDto> ObtenerCombo(int idEstado)
        {
            List<UsuarioObtenerComboDto> resultado = new List<UsuarioObtenerComboDto>();
            try
            {
                const string query = "Seguridad.usp_Usuario_ObtenerCombo";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<UsuarioObtenerComboDto>(query, new
                    {
                        IdEstado = idEstado
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public UsuarioObtenerContraseniaPorIdDto ObtenerContraseniaPorId(long id)
        {
            UsuarioObtenerContraseniaPorIdDto resultado = new UsuarioObtenerContraseniaPorIdDto();
            try
            {
                const string query = "Seguridad.usp_Usuario_ObtenerContraseniaPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<UsuarioObtenerContraseniaPorIdDto>(query, new
                    {
                        IdUsuario = id
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
