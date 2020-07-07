using Datos.Repositorio.Transaccion;
using Entidad.Configuracion.Proceso;
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
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return respuesta;
        }

        public int Modificar(RequestPedidoModificarDto modelo)
        {
            return _adPedido.Modificar(modelo);
        }

        public PedidoObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adPedido.ObtenerPorId(id);
        }
    }
}
