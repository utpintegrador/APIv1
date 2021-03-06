﻿using Dapper;
using Datos.Helper;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Dto.Transaccion;
using Entidad.Request.Transaccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Datos.Repositorio.Transaccion
{
    public class AdPedido: Logger
    {
        public List<PedidoObtenerPorIdNegocioCompradorDto> ObtenerPorIdNegocioComprador(RequestPedidoObtenerPorIdNegocioCompradorDto filtro)
        {
            List<PedidoObtenerPorIdNegocioCompradorDto> resultado = new List<PedidoObtenerPorIdNegocioCompradorDto>();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerPorIdNegocioComprador";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoObtenerPorIdNegocioCompradorDto>(query, new
                    {
                        filtro.IdNegocioComprador,
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdMoneda,
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

        public List<PedidoObtenerPorIdNegocioVendedorDto> ObtenerPorIdNegocioVendedor(RequestPedidoObtenerPorIdNegocioVendedorDto filtro)
        {
            List<PedidoObtenerPorIdNegocioVendedorDto> resultado = new List<PedidoObtenerPorIdNegocioVendedorDto>();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerPorIdNegocioVendedor";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoObtenerPorIdNegocioVendedorDto>(query, new
                    {
                        filtro.IdNegocioVendedor,
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdMoneda,
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

        public List<PedidoObtenerComprasPorIdUsuarioDto> ObtenerComprasPorIdUsuario(RequestPedidoObtenerComprasPorIdUsuarioDto filtro)
        {
            List<PedidoObtenerComprasPorIdUsuarioDto> resultado = new List<PedidoObtenerComprasPorIdUsuarioDto>();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerComprasPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoObtenerComprasPorIdUsuarioDto>(query, new
                    {
                        filtro.IdUsuario,
                        filtro.IdNegocioComprador,
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdMoneda,
                        filtro.NumeroPagina,
                        filtro.CantidadRegistros,
                        filtro.ColumnaOrden,
                        filtro.DireccionOrden,
                        FechaDesde = filtro.FechaDesdeDate,
                        FechaHasta = filtro.FechaHastaDate
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<PedidoObtenerVentasPorIdUsuarioDto> ObtenerVentasPorIdUsuario(RequestPedidoObtenerVentasPorIdUsuarioDto filtro)
        {
            List<PedidoObtenerVentasPorIdUsuarioDto> resultado = new List<PedidoObtenerVentasPorIdUsuarioDto>();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerVentasPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoObtenerVentasPorIdUsuarioDto>(query, new
                    {
                        filtro.IdUsuario,
                        filtro.IdNegocioVendedor,
                        filtro.Buscar,
                        filtro.IdEstado,
                        filtro.IdMoneda,
                        filtro.NumeroPagina,
                        filtro.CantidadRegistros,
                        filtro.ColumnaOrden,
                        filtro.DireccionOrden,
                        FechaDesde = filtro.FechaDesdeDate,
                        FechaHasta = filtro.FechaHastaDate
                    }, commandType: CommandType.StoredProcedure).ToList();

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Registrar(RequestPedidoRegistrarDto modelo, ref long idNuevo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_Pedido_Registrar";
                var p = new DynamicParameters();
                p.Add("IdPedido", 0, DbType.Int64, ParameterDirection.Output);
                p.Add("IdNegocioVendedor", modelo.IdNegocioVendedor);
                p.Add("IdNegocioComprador", modelo.IdNegocioComprador);
                p.Add("Direccion", modelo.Direccion);
                p.Add("IdMoneda", modelo.IdMoneda);
                p.Add("IdUsuarioRegistro", modelo.IdUsuarioRegistro);
                p.Add("NumeroCelular", modelo.NumeroCelular);
                p.Add("Observaciones", modelo.Observaciones);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                    idNuevo = p.Get<long>("IdPedido");
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int Modificar(RequestPedidoModificarDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_Pedido_Modificar";
                var p = new DynamicParameters();
                p.Add("IdPedido", modelo.IdPedido);
                p.Add("IdNegocioVendedor", modelo.IdNegocioVendedor);
                p.Add("IdNegocioComprador", modelo.IdNegocioComprador);
                p.Add("Direccion", modelo.Direccion);
                p.Add("IdMoneda", modelo.IdMoneda);
                p.Add("IdUsuarioRegistro", modelo.IdUsuarioRegistro);
                p.Add("NumeroCelular", modelo.NumeroCelular);
                p.Add("Observaciones", modelo.Observaciones);

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, commandType: CommandType.StoredProcedure, param: p);
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public PedidoObtenerPorIdDto ObtenerPorId(long id)
        {
            PedidoObtenerPorIdDto resultado = new PedidoObtenerPorIdDto();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerPorId";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PedidoObtenerPorIdDto>(query, new
                    {
                        IdPedido = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public int ModificarEstadoPorParteDeComprador(RequestPedidoModificarEstadoPorParteDeCompradorDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_Pedido_ModificarEstadoPorParteDeComprador";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query,new {
                        modelo.IdPedido,
                        modelo.IdNegocioComprador,
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

        public int ModificarEstadoPorParteDeVendedor(RequestPedidoModificarEstadoPorParteDeVendedorDto modelo)
        {
            int resultado = 0;
            try
            {
                const string query = "Transaccion.usp_Pedido_ModificarEstadoPorParteDeVendedor";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed) cn.Open();

                    resultado = cn.Execute(query, new
                    {
                        modelo.IdPedido,
                        modelo.IdNegocioVendedor,
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

        public PedidoObtenerNotaPedidoDto ObtenerNotaPedido(long id)
        {
            PedidoObtenerNotaPedidoDto resultado = new PedidoObtenerNotaPedidoDto();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerNotaPedido";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.QuerySingleOrDefault<PedidoObtenerNotaPedidoDto>(query, new
                    {
                        IdPedido = id
                    }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                Log(Level.Error, (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return resultado;
        }

        public List<PedidoObtenerPendientesAtencionPorIdUsuarioDto> ObtenerPendientesAtencionPorIdUsuario(long idUsuario)
        {
            List<PedidoObtenerPendientesAtencionPorIdUsuarioDto> resultado = new List<PedidoObtenerPendientesAtencionPorIdUsuarioDto>();
            try
            {
                const string query = "Transaccion.usp_Pedido_ObtenerPendientesAtencionPorIdUsuario";

                using (var cn = HelperClass.ObtenerConeccion())
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    resultado = cn.Query<PedidoObtenerPendientesAtencionPorIdUsuarioDto>(query, new
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

        //public List<PedidoObtenerPorIdConDetallesAgrupadoDto> ObtenerPorIdConDetalles(long id)
        //{
        //    List<PedidoObtenerPorIdConDetallesAgrupadoDto> resultado = new List<PedidoObtenerPorIdConDetallesAgrupadoDto>();
        //    try
        //    {
        //        const string query = "Transaccion.usp_Pedido_ObtenerPorIdConDetalles";

        //        using (var cn = HelperClass.ObtenerConeccion())
        //        {
        //            if (cn.State == ConnectionState.Closed)
        //            {
        //                cn.Open();
        //            }

        //            resultado = cn.Query<PedidoObtenerPorIdConDetallesAgrupadoDto>(query, new
        //            {
        //                IdPedido = id
        //            }, commandType: CommandType.StoredProcedure).ToList();

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
