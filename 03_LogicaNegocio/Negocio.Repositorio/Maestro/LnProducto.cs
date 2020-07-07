using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnProducto
    {
        private readonly AdProducto _adProducto = new AdProducto();
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

        public ProductoObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adProducto.ObtenerPorId(id);
        }

        public int Registrar(RequestProductoRegistrarDto modelo, ref long idNuevo)
        {
            return _adProducto.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestProductoModificarDto modelo)
        {
            return _adProducto.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adProducto.Eliminar(id);
        }
    }
}
