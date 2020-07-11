using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Repositorio.Maestro
{
    public class LnProducto
    {
        private readonly AdProducto _adProducto = new AdProducto();

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

        public ProductoAtributoDto ObtenerPorIdConAtributos(long id)
        {
            var lista = _adProducto.ObtenerPorIdConAtributos(id);
            if (lista.Any())
            {
                ProductoAtributoDto producto = (from prod in lista
                                                select new ProductoAtributoDto
                                                {
                                                    IdProducto = prod.IdProducto,
                                                    Descripcion = prod.Descripcion,
                                                    DescripcionExtendida = prod.DescripcionExtendida,
                                                    Precio = prod.Precio,
                                                    IdMoneda = prod.IdMoneda,
                                                    IdCategoria = prod.IdCategoria,
                                                    IdNegocio = prod.IdNegocio,
                                                    IdEstado = prod.IdEstado
                                                }).Distinct().First();

                List<ProductoAtributoDescuentoDto> listaDescuento = (from descu in lista
                                                                     where descu.IdProductoDescuento != null
                                                                     select new ProductoAtributoDescuentoDto
                                                                     {
                                                                         IdProductoDescuento = descu.IdProductoDescuento,
                                                                         FechaInicio = descu.FechaInicio,
                                                                         FechaFin = descu.FechaFin,
                                                                         Valor = descu.Valor,
                                                                         DescripcionTipoDescuento = descu.DescripcionTipoDescuento,
                                                                         DescripcionEstadoDescuento = descu.DescripcionEstadoDescuento
                                                                     }).Distinct().ToList();

                List<ProductoAtributoImagenDto> listaImagen = (from ima in lista
                                                               where ima.IdProductoImagen != null
                                                               select new ProductoAtributoImagenDto
                                                               {
                                                                   IdProductoImagen = ima.IdProductoImagen,
                                                                   UrlImagen = ima.UrlImagen,
                                                                   Predeterminado = ima.Predeterminado
                                                               }).Distinct().ToList();

                producto.ListaDescuento = new List<ProductoAtributoDescuentoDto>();
                producto.ListaImagen = new List<ProductoAtributoImagenDto>();
                producto.ListaDescuento = listaDescuento;
                producto.ListaImagen = listaImagen;

                return producto;
            }
            return null;
        }

    }
}
