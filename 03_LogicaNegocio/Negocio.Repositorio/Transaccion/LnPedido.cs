using Datos.Repositorio.Transaccion;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Dto.Transaccion;
using Entidad.Request.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Negocio.Repositorio.Transaccion
{
    public class LnPedido: Logger
    {
        private readonly AdPedido _adPedido = new AdPedido();
        public List<PedidoObtenerPorIdNegocioCompradorDto> ObtenerPorIdNegocioComprador(RequestPedidoObtenerPorIdNegocioCompradorDto filtro)
        {
            if (filtro == null) filtro = new RequestPedidoObtenerPorIdNegocioCompradorDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdPedido";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adPedido.ObtenerPorIdNegocioComprador(filtro);
            if (listado == null)
            {
                listado = new List<PedidoObtenerPorIdNegocioCompradorDto>();
            }
            return listado;
        }

        public List<PedidoObtenerPorIdNegocioVendedorDto> ObtenerPorIdNegocioVendedor(RequestPedidoObtenerPorIdNegocioVendedorDto filtro)
        {
            if (filtro == null) filtro = new RequestPedidoObtenerPorIdNegocioVendedorDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdPedido";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adPedido.ObtenerPorIdNegocioVendedor(filtro);
            if (listado == null)
            {
                listado = new List<PedidoObtenerPorIdNegocioVendedorDto>();
            }
            return listado;
        }

        public List<PedidoObtenerComprasPorIdUsuarioDto> ObtenerComprasPorIdUsuario(RequestPedidoObtenerComprasPorIdUsuarioDto filtro)
        {
            if (filtro == null) filtro = new RequestPedidoObtenerComprasPorIdUsuarioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdPedido";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adPedido.ObtenerComprasPorIdUsuario(filtro);
            if (listado == null)
            {
                listado = new List<PedidoObtenerComprasPorIdUsuarioDto>();
            }
            return listado;
        }

        public List<PedidoObtenerVentasPorIdUsuarioDto> ObtenerVentasPorIdUsuario(RequestPedidoObtenerVentasPorIdUsuarioDto filtro)
        {
            if (filtro == null) filtro = new RequestPedidoObtenerVentasPorIdUsuarioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdPedido";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adPedido.ObtenerVentasPorIdUsuario(filtro);
            if (listado == null)
            {
                listado = new List<PedidoObtenerVentasPorIdUsuarioDto>();
            }
            return listado;
        }

        public int Registrar(RequestPedidoRegistrarDto modelo, ref long idNuevo)
        {
            int respuesta = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    respuesta = _adPedido.Registrar(modelo, ref idNuevo);
                    LnPedidoDetalle lnPedidoDetalle = new LnPedidoDetalle();
                    if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<RequestPedidoRegistrarPedidoDetalleRegistrarDto>();

                    if (respuesta > 0 && idNuevo > 0)
                    {
                        if (modelo.ListaPedidoDetalle.Any())
                        {
                            int cantidadOkEsperadas = modelo.ListaPedidoDetalle.Count;
                            int cantidadDetallesOk = 0;
                            foreach (var det in modelo.ListaPedidoDetalle)
                            {
                                var entUbi = new RequestPedidoDetalleRegistrarDto
                                {
                                    IdPedido = idNuevo,
                                    Cantidad = det.Cantidad,
                                    IdProducto = det.IdProducto,
                                    PrecioUnitario = det.PrecioUnitario
                                };

                                long idNuevoDetalle = 0;
                                int resultadoDetalle = lnPedidoDetalle.Registrar(entUbi, ref idNuevoDetalle);
                                if (resultadoDetalle > 0 && idNuevoDetalle > 0)
                                {
                                    cantidadDetallesOk++;
                                }
                            }
                            if (cantidadOkEsperadas == cantidadDetallesOk)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return respuesta;
        }

        public int Modificar(RequestPedidoModificarDto modelo)
        {
            //return _adPedido.Modificar(modelo);
            int respuesta = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    respuesta = _adPedido.Modificar(modelo);
                    LnPedidoDetalle lnPedidoDetalle = new LnPedidoDetalle();
                    if (modelo.ListaPedidoDetalle == null) modelo.ListaPedidoDetalle = new List<RequestPedidoModificarPedidoDetalleModificarDto>();
                    modelo.ListaPedidoDetalle = modelo.ListaPedidoDetalle
                        .Where(x => x.Accion == "add" || x.Accion == "upd" || x.Accion == "del").ToList();

                    if (respuesta > 0)
                    {
                        if (modelo.ListaPedidoDetalle.Any())
                        {
                            int cantidadOkEsperadas = modelo.ListaPedidoDetalle.Count;
                            int cantidadDetallesOk = 0;
                            foreach (var det in modelo.ListaPedidoDetalle)
                            {
                                switch (det.Accion)
                                {
                                    case "add":
                                        {
                                            var entUbi = new RequestPedidoDetalleRegistrarDto
                                            {
                                                IdPedido = modelo.IdPedido,
                                                Cantidad = det.Cantidad,
                                                IdProducto = det.IdProducto,
                                                PrecioUnitario = det.PrecioUnitario
                                            };

                                            long idNuevoDetalle = 0;
                                            int resultadoDetalle = lnPedidoDetalle.Registrar(entUbi, ref idNuevoDetalle);
                                            if (resultadoDetalle > 0 && idNuevoDetalle > 0)
                                            {
                                                cantidadDetallesOk++;
                                            }
                                            break;
                                        }
                                    case "upd":
                                        {
                                            var entUbi = new RequestPedidoDetalleModificarDto
                                            {
                                                IdPedidoDetalle = det.IdPedidoDetalle,
                                                IdPedido = modelo.IdPedido,
                                                Cantidad = det.Cantidad,
                                                IdProducto = det.IdProducto,
                                                PrecioUnitario = det.PrecioUnitario
                                            };

                                            int resultadoDetalle = lnPedidoDetalle.Modificar(entUbi);
                                            if (resultadoDetalle > 0)
                                            {
                                                cantidadDetallesOk++;
                                            }
                                            break;
                                        }
                                    case "del":
                                        {
                                            int resultadoDetalle = lnPedidoDetalle.Eliminar(det.IdPedidoDetalle);
                                            if (resultadoDetalle > 0)
                                            {
                                                cantidadDetallesOk++;
                                            }
                                            break;
                                        }
                                }
                            }

                            if (cantidadOkEsperadas == cantidadDetallesOk)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return respuesta;
        }

        public PedidoObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adPedido.ObtenerPorId(id);
        }

        public int ModificarEstadoPorParteDeComprador(RequestPedidoModificarEstadoPorParteDeCompradorDto modelo)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                respuesta = _adPedido.ModificarEstadoPorParteDeComprador(modelo);
                if (respuesta > 0)
                {
                    scope.Complete();
                }
            }
            return respuesta;
        }

        public int ModificarEstadoPorParteDeVendedor(RequestPedidoModificarEstadoPorParteDeVendedorDto modelo)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                respuesta = _adPedido.ModificarEstadoPorParteDeVendedor(modelo);
                if (respuesta > 0)
                {
                    scope.Complete();
                }
            }
            return respuesta;
        }

        public PedidoAtributoDto ObtenerPorIdConDetalles(long id)
        {
            PedidoAtributoDto pedido = null;
            var pedidoCabecera = _adPedido.ObtenerPorId(id);
            if (pedidoCabecera != null)
            {
                LnPedidoDetalle lnPedidoDetalle = new LnPedidoDetalle();
                var listaDetalles = lnPedidoDetalle.ObtenerPorIdPedido(id);
                
                List<PedidoAtributoDetalleDto> listaDet = new List<PedidoAtributoDetalleDto>();
                if (listaDetalles != null)
                {
                    if (listaDetalles.Any())
                    {
                        listaDet = (from tab in listaDetalles
                                     select new PedidoAtributoDetalleDto
                                     {
                                         IdPedidoDetalle = tab.IdPedidoDetalle,
                                         Cantidad = tab.Cantidad,
                                         DescripcionProducto = tab.DescripcionProducto,
                                         PrecioUnitario = tab.PrecioUnitario,
                                         UrlImagenProducto = tab.UrlImagenProducto
                                     }).ToList();
                    }
                }

                pedido = new PedidoAtributoDto
                {
                    IdPedido = pedidoCabecera.IdPedido,
                    Direccion = pedidoCabecera.Direccion,
                    IdEstado = pedidoCabecera.IdEstado,
                    IdMoneda = pedidoCabecera.IdMoneda,
                    IdNegocioComprador = pedidoCabecera.IdNegocioComprador,
                    IdNegocioVendedor = pedidoCabecera.IdNegocioVendedor,
                    Total = pedidoCabecera.Total,
                    NumeroCelular = pedidoCabecera.NumeroCelular,
                    Observaciones = pedidoCabecera.Observaciones,
                    ListaDetalle = listaDet
                };

                return pedido;
            }

            return null;
            
        }

        public PedidoObtenerNotaPedidoDto ObtenerNotaPedidoPorId(long id)
        {
            PedidoObtenerNotaPedidoDto pedidoCabecera = _adPedido.ObtenerNotaPedido(id);
            if (pedidoCabecera != null)
            {
                LnPedidoDetalle lnPedidoDetalle = new LnPedidoDetalle();
                var listaDetalles = lnPedidoDetalle.ObtenerPorIdPedido(id);

                List<PedidoAtributoDetalleDto> listaDet = new List<PedidoAtributoDetalleDto>();
                if (listaDetalles != null)
                {
                    if (listaDetalles.Any())
                    {
                        listaDet = (from tab in listaDetalles
                                    select new PedidoAtributoDetalleDto
                                    {
                                        IdPedidoDetalle = tab.IdPedidoDetalle,
                                        Cantidad = tab.Cantidad,
                                        DescripcionProducto = tab.DescripcionProducto,
                                        PrecioUnitario = tab.PrecioUnitario,
                                        UrlImagenProducto = tab.UrlImagenProducto
                                    }).ToList();

                        pedidoCabecera.ListaDetalle = listaDet;
                    }
                }

                return pedidoCabecera;
            }

            return null;

        }

        public List<PedidoObtenerPendientesAtencionPorIdUsuarioDto> ObtenerPendientesAtencionPorIdUsuario(long idUsuario)
        {
            var listado = _adPedido.ObtenerPendientesAtencionPorIdUsuario(idUsuario);
            if(listado == null)
            {
                listado = new List<PedidoObtenerPendientesAtencionPorIdUsuarioDto>();
            }
            return listado;
        }

        //public PedidoObtenerNotaPedidoDto ObtenerNotaPedido(long id)

    }
}
