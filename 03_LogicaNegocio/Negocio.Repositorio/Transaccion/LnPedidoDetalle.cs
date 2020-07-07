using Datos.Repositorio.Transaccion;
using Entidad.Request.Transaccion;
using System.Transactions;

namespace Negocio.Repositorio.Transaccion
{
    public class LnPedidoDetalle
    {
        private readonly AdPedidoDetalle _adPedidoDetalle = new AdPedidoDetalle();

        public int ProcesarRegistro(RequestPedidoDetalleRootRegistrarDto modelo)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                int cantidadOkEsperadas = modelo.ListaPedidoDetalle.Count;
                int cantidadDetallesOk = 0;
                foreach (var det in modelo.ListaPedidoDetalle)
                {
                    long idNuevoDetalle = 0;
                    int resultadoDetalle = Registrar(det, ref idNuevoDetalle);
                    if (resultadoDetalle > 0 && idNuevoDetalle > 0)
                    {
                        cantidadDetallesOk++;
                    }
                }
                if (cantidadOkEsperadas == cantidadDetallesOk)
                {
                    scope.Complete();
                    respuesta = 1;
                }
            }
            return respuesta;
        }

        public int ProcesarModificacion(RequestPedidoDetalleRootModificarDto modelo)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                int cantidadOkEsperadas = modelo.ListaPedidoDetalle.Count;
                int cantidadDetallesOk = 0;
                foreach (var det in modelo.ListaPedidoDetalle)
                {
                    int resultadoDetalle = Modificar(det);
                    if (resultadoDetalle > 0)
                    {
                        cantidadDetallesOk++;
                    }
                }
                if (cantidadOkEsperadas == cantidadDetallesOk)
                {
                    scope.Complete();
                    respuesta = 1;
                }
            }
            return respuesta;
        }

        public int ProcesarEliminacion(RequestPedidoDetalleRootEliminarDto modelo)
        {
            int respuesta = 0;
            using (var scope = new TransactionScope())
            {
                int cantidadOkEsperadas = modelo.ListaPedidoDetalle.Count;
                int cantidadDetallesOk = 0;
                foreach (var det in modelo.ListaPedidoDetalle)
                {
                    int resultadoDetalle = Eliminar(det);
                    if (resultadoDetalle > 0)
                    {
                        cantidadDetallesOk++;
                    }
                }
                if (cantidadOkEsperadas == cantidadDetallesOk)
                {
                    scope.Complete();
                    respuesta = 1;
                }
            }
            return respuesta;
        }

        public int Registrar(RequestPedidoDetalleRegistrarDto modelo, ref long idNuevo)
        {
            return _adPedidoDetalle.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestPedidoDetalleModificarDto modelo)
        {
            return _adPedidoDetalle.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adPedidoDetalle.Elminar(id);
        }
    }
}
