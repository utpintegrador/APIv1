using Datos.Repositorio.Maestro;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Negocio.Repositorio.Maestro
{
    public class LnProducto: Logger
    {
        private readonly AdProducto _adProducto = new AdProducto();

        //Producto por ID Usuario
        public List<ProductoObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(RequestProductoObtenerPorIdUsuarioDto filtro)
        {
            if (filtro == null) filtro = new RequestProductoObtenerPorIdUsuarioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdProducto";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adProducto.ObtenerPorIdUsuario(filtro);
            if (listado == null)
            {
                listado = new List<ProductoObtenerPorIdUsuarioDto>();
            }
            return listado;
        }

        //Producto por ID Negocio
        public List<ProductoObtenerPorIdNegocioDto> ObtenerPorIdNegocio(RequestProductoObtenerPorIdNegocioDto filtro)
        {
            if (filtro == null) filtro = new RequestProductoObtenerPorIdNegocioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdProducto";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adProducto.ObtenerPorIdNegocio(filtro);
            if (listado == null)
            {
                listado = new List<ProductoObtenerPorIdNegocioDto>();
            }
            return listado;
        }

        //Obtener Producto por ID
        public ProductoObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adProducto.ObtenerPorId(id);
        }

        //Registrar Producto
        public int Registrar(RequestProductoRegistrarDto modelo, ref long idNuevo)
        {
            return _adProducto.Registrar(modelo, ref idNuevo);
        }


        //Modificar Producto
        public int Modificar(RequestProductoModificarDto modelo)
        {
            return _adProducto.Modificar(modelo);
        }

        //Eliminar Producto
        public int Eliminar(long id)
        {
            return _adProducto.Eliminar(id);
        }

        //Obtener Producto - Listar
        public ProductoAtributoDto ObtenerPorIdConAtributos(long id)
        {
            ProductoAtributoDto producto = null;
            var productoCabecera = _adProducto.ObtenerPorId(id);
            if(productoCabecera != null)
            {
                LnProductoImagen lnProductoImagen = new LnProductoImagen();
                var listaImagenes = lnProductoImagen.ObtenerPorIdProducto(new RequestProductoImagenObtenerPorIdProductoDto
                {
                    CantidadRegistros = 100,
                    IdProducto = productoCabecera.IdProducto
                });

                LnProductoDescuento lnProductoDescuento = new LnProductoDescuento();
                var listaDescuentos = lnProductoDescuento.ObtenerPorIdProducto(new RequestProductoDescuentoObtenerPorIdProductoDto
                {
                    IdProducto = productoCabecera.IdProducto,
                    CantidadRegistros = 100
                });

                List<ProductoAtributoDescuentoDto> listaDesc = new List<ProductoAtributoDescuentoDto>();
                if(listaDescuentos != null)
                {
                    if (listaDescuentos.Any())
                    {
                        listaDesc = (from tab in listaDescuentos
                                     select new ProductoAtributoDescuentoDto
                                     {
                                         IdProductoDescuento = tab.IdProductoDescuento,
                                         FechaInicio = tab.FechaInicio,
                                         FechaFin = tab.FechaFin,
                                         DescripcionEstadoDescuento = tab.DescripcionEstado,
                                         DescripcionTipoDescuento = tab.DescripcionTipoDescuento,
                                         Valor = tab.Valor
                                     }).ToList();
                    }
                }

                List<ProductoAtributoImagenDto> listaIma = new List<ProductoAtributoImagenDto>();
                if (listaImagenes != null)
                {
                    if (listaImagenes.Any())
                    {
                        listaIma = (from tab in listaImagenes
                                    select new ProductoAtributoImagenDto
                                    {
                                        IdProductoImagen = tab.IdProductoImagen,
                                        UrlImagen = tab.UrlImagen,
                                        Predeterminado = tab.Predeterminado
                                    }).ToList();
                    }
                }

                producto = new ProductoAtributoDto
                {
                    IdProducto = productoCabecera.IdProducto,
                    Descripcion = productoCabecera.Descripcion,
                    DescripcionExtendida = productoCabecera.DescripcionExtendida,
                    IdCategoria = productoCabecera.IdCategoria,
                    IdEstado = productoCabecera.IdEstado,
                    IdMoneda = productoCabecera.IdMoneda,
                    IdNegocio = productoCabecera.IdNegocio,
                    Precio = productoCabecera.Precio,
                    IdUsuario= productoCabecera.IdUsuario,
                    ListaDescuento = listaDesc,
                    ListaImagen = listaIma
                };

                return producto;
            }

            return null;
        }

        //Eliminar masivo
        public int EliminarMasivo(RequestProductoEliminarMasivoDto prm)
        {
            int respuesta = 0;
            try
            {
                int cantidadaEliminar = prm.ListaIdProducto.Count();
                int contadorOk = 0;

                using (var scope = new TransactionScope())
                {
                    foreach (var producto in prm.ListaIdProducto)
                    {
                        var resultadoTemp = Eliminar(producto.IdProducto);
                        if (resultadoTemp > 0)
                        {
                            contadorOk++;
                        }
                    }

                    if (cantidadaEliminar == contadorOk)
                    {
                        scope.Complete();
                        respuesta = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                respuesta = 0;
            }
            return respuesta;            
        }

    }
}
