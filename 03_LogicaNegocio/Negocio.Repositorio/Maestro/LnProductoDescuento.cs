﻿using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnProductoDescuento
    {
        private readonly AdProductoDescuento _adProductoDescuento = new AdProductoDescuento();
        public List<ProductoDescuentoObtenerPorIdProductoDto> ObtenerPorIdProducto(RequestProductoDescuentoObtenerPorIdProductoDto filtro)
        {
            if (filtro == null) filtro = new RequestProductoDescuentoObtenerPorIdProductoDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "FechaInicio";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var lista = _adProductoDescuento.ObtenerPorIdProducto(filtro);
            if (lista == null)
            {
                lista = new List<ProductoDescuentoObtenerPorIdProductoDto>();
            }
            return lista;
        }
    }
}